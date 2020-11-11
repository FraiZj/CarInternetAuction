using AutoMapper;
using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Validation;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InternetAuction.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private ICollection<CarImage> GetUploadedImages(ICollection<HttpPostedFileBase> files)
        {
            var carImages = new List<CarImage>();

            foreach (var file in files)
            {
                var img = new CarImage();
                img.Title = file.FileName;

                using (var ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    img.Data = ms.ToArray();
                    ms.Close();
                }

                carImages.Add(img);
            }

            return carImages;
        }

        private ICollection<ImageModel> GetRetrievedImages(ICollection<CarImage> carImages)
        {
            var imageModels = new List<ImageModel>();
            foreach (var image in carImages)
            {
                var imageModel = new ImageModel
                {
                    Url = $"data:image/jpg;base64,{Convert.ToBase64String(image.Data)}",
                    Title = image.Title
                };
                imageModels.Add(imageModel);
            }
            return imageModels;
        }

        public async Task<OperationDetails> AddAsync(LotModel model)
        {
            try
            {
                if (!ValidateLotModel(model, out var validationResult))
                    return new OperationDetails(false, validationResult);

                var lot = _mapper.Map<Lot>(model);
                lot.Car.CarImages = GetUploadedImages(model.Car.Files);
                _unitOfWork.LotRepository.Add(lot);
                await _unitOfWork.SaveAsync();

                return new OperationDetails(true, returnValue: lot.Id);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while adding a new lot", ex.InnerException);
            }
        }

        public async Task<OperationDetails> DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.LotRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while deleting a lot", ex.InnerException);
            }
        }

        public IQueryable<LotModel> GetAll()
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAll().ToList();
                return _mapper.Map<List<LotModel>>(lots).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        public IQueryable<LotModel> GetAllActiveLots()
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAll().Where(l => l.IsActive).ToList();
                return _mapper.Map<List<LotModel>>(lots).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        public IQueryable<LotModel> GetAllActiveLotsWithDetails()
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAll().Where(l => l.IsActive);
                return _mapper.Map<IQueryable<LotModel>>(lots);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        public IQueryable<LotModel> GetAllWithDetails()
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAllWithDetails();
                return _mapper.Map<IQueryable<LotModel>>(lots);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        public async Task<LotModel> GetByIdAsync(int id)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdAsync(id);
                return _mapper.Map<LotModel>(lot);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching the lot", ex.InnerException);
            }
        }

        public async Task<LotModel> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(id);
                var lotModel = _mapper.Map<LotModel>(lot);
                lotModel.Car.CarImages = GetRetrievedImages(lot.Car.CarImages);
                return lotModel;
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching the lot", ex.InnerException);
            }
        }

        public async Task<OperationDetails> UpdateAsync(LotModel model)
        {
            try
            {
                if (!ValidateLotModel(model, out var validationResult))
                    return new OperationDetails(false, validationResult);

                var lot = _mapper.Map<Lot>(model);
                _unitOfWork.LotRepository.Update(lot);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, returnValue: lot.Id);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while updating lot", ex.InnerException);
            }
        }

        //public async Task<OperationDetails> CloseLotAsync(int lotId, string buyerId, decimal sum)
        //{
        //    try
        //    {
        //        var lot = await GetByIdWithDetailsAsync(lotId);

        //        if (!ValidateLotModel(lot, out var validationResult))
        //            return new OperationDetails(false, validationResult);

        //        lot.BuyerId = buyerId;
        //        //TODO: implement 
        //        //lot.Bets.Add(new Bet { });

        //        _unitOfWork.LotRepository.Update(lot);
        //        await _unitOfWork.SaveAsync();
        //        return new OperationDetails(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InternetAuctionException("An error occured while updating lot", ex.InnerException);
        //    }
        //}

        private ICollection<ValidationResult> Validate(object model)
        {
            var validationResult = new List<ValidationResult>();
            Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), validationResult, true);
            return validationResult;
        }

        private bool ValidateLotModel(LotModel model, out ICollection<ValidationResult> validationResult)
        {
            if (model is null)
            {
                validationResult = new List<ValidationResult> { new ValidationResult("LotModel cannot be null") };
                return false;
            }

            if (model.Car is null)
            {
                validationResult = new List<ValidationResult> { new ValidationResult("Car cannot be null") };
                return false;
            }

            if (model.Car.TechnicalPassport is null)
            {
                validationResult = new List<ValidationResult> { new ValidationResult("TechnicalPassport cannot be null") };
                return false;
            }

            var modelValidationResult = Validate(model);
            var carValidationResult = Validate(model.Car);
            var technicalPassportValidationResult = Validate(model.Car.TechnicalPassport);

            validationResult = modelValidationResult.Concat(carValidationResult).Concat(technicalPassportValidationResult).ToList();

            if (model.StartPrice < 0)
                validationResult.Add(new ValidationResult("Invalid Start Price", new List<string> { "StartPrice" }));

            if (model.TurnkeyPrice < 0)
                validationResult.Add(new ValidationResult("Invalid Turnkey Price", new List<string> { "TurnkeyPrice" }));

            if (model.Car.Year > DateTime.UtcNow.Year)
                validationResult.Add(new ValidationResult("Invalid Car Year", new List<string> { "Car", "Year" }));

            return validationResult.Count == 0;
        }
    }
}
