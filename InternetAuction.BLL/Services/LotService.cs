using AutoMapper;
using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Validation;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Enums;
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
    /// <summary>
    /// Represents lot service class
    /// </summary>
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool isDisposed;

        /// <summary>
        /// Initializes lot service with unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public LotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a lot model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ReturnValue type - int</returns>
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

        /// <summary>
        /// Deletes a bet model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationDetails> DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.LotRepository.DeleteByIdAsync(id);
                await _unitOfWork.CarRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while deleting a lot", ex.InnerException);
            }
        }

        /// <summary>
        /// Returns all lots models
        /// </summary>
        /// <returns></returns>
        public IQueryable<LotModel> GetAll()
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAllWithDetails().ToList();
                var lotsModels = _mapper.Map<List<LotModel>>(lots);

                for (int i = 0; i < lots.Count; i++)
                {
                    lotsModels[i].Car.CarImages = GetRetrievedImages(lots[i].Car.CarImages);
                }

                return lotsModels.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        /// <summary>
        /// Returns a lot model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LotModel> GetByIdAsync(int id)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(id);

                if (lot is null)
                    return null;

                var lotModel = _mapper.Map<LotModel>(lot);
                lotModel.Car.CarImages = GetRetrievedImages(lot.Car.CarImages);
                return lotModel;
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching the lot", ex.InnerException);
            }
        }

        /// <summary>
        /// Updates lot model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ReturnValue type - LotModel</returns>
        public async Task<OperationDetails> UpdateAsync(LotModel model)
        {
            try
            {
                if (!ValidateLotModel(model, out var validationResult))
                    return new OperationDetails(false, validationResult);

                var updatedLot = _mapper.Map<Lot>(model);

                _unitOfWork.LotRepository.Update(updatedLot);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, returnValue: model);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while updating lot", ex.InnerException);
            }
        }

        /// <summary>
        /// Returns lots models by search model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IQueryable<LotModel> SearchLotModels(SearchModel model)
        {
            try
            {
                var lots = _unitOfWork.LotRepository.FindAll().ToList();

                if (model != null)
                {
                    var carBrand = model.Brand?.Trim().ToUpper();
                    if (!string.IsNullOrWhiteSpace(carBrand))
                        lots = lots.Where(l => l.Car.Brand.Trim().ToUpper().Contains(carBrand)).ToList();

                    var carModel = model.CarModel?.Trim().ToUpper();
                    if (!string.IsNullOrWhiteSpace(carModel))
                        lots = lots.Where(l => l.Car.Model.Trim().ToUpper().Contains(carModel)).ToList();

                    var selectedBodyType = _mapper.Map<BodyType>(model.BodyType);
                    if (model.BodyType != 0)
                        lots = lots.Where(l => l.Car.TechnicalPassport.BodyType == selectedBodyType).ToList();

                    var selectedDriveUnit = _mapper.Map<DriveUnit>(model.DriveUnit);
                    if (model.DriveUnit != 0)
                        lots = lots.Where(l => l.Car.TechnicalPassport.DriveUnit == selectedDriveUnit).ToList();

                    if (model.MaxPrice >= model.MinPrice)
                        lots = lots.Where(l => l.StartPrice >= model.MinPrice
                                        && l.StartPrice <= model.MaxPrice).ToList();
                }

                var lotsModels = _mapper.Map<List<LotModel>>(lots);

                for (int i = 0; i < lots.Count; i++)
                {
                    lotsModels[i].Car.CarImages = GetRetrievedImages(lots[i].Car.CarImages);
                }

                return lotsModels.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching lots", ex.InnerException);
            }
        }

        /// <summary>
        /// Sells lot for selected bet
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="betId"></param>
        /// <returns></returns>
        public async Task<OperationDetails> SellLot(int lotId, int betId)
        {
            try
            {
                var bet = await _unitOfWork.BetRepository.GetByIdAsync(betId);

                if (bet is null)
                    return new OperationDetails(false, new List<ValidationResult> { new ValidationResult($"Bet with Id = {betId} does not exist") });

                var lot = await _unitOfWork.LotRepository.GetByIdAsync(lotId);

                if (lot is null)
                    return new OperationDetails(false, new List<ValidationResult> { new ValidationResult($"Lot with Id = {lotId} does not exist") });

                if (lot.Id != bet.LotId)
                    return new OperationDetails(false, new List<ValidationResult> { new ValidationResult($"The bet does not match the lot") });

                lot.BuyerId = bet.UserId;
                lot.IsActive = false;

                _unitOfWork.LotRepository.Update(lot);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, returnValue: lot.Id);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while selling lot", ex);
            }
        }

        /// <summary>
        /// Marks the lot as bought by the specified user
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OperationDetails> BuyLot(int lotId, string userId)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdAsync(lotId);

                if (lot is null)
                    return new OperationDetails(false, new List<ValidationResult> { new ValidationResult($"Lot with Id = {lotId} does not exist") });

                lot.BuyerId = userId;
                lot.IsActive = false;

                _unitOfWork.LotRepository.Update(lot);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, returnValue: lot.Id);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while buying lot", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                _unitOfWork.Dispose();
            }

            isDisposed = true;
        }

        /// <summary>
        /// Transforms Http Posted Files to Car Images
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private ICollection<CarImage> GetUploadedImages(ICollection<HttpPostedFileBase> files)
        {
            var carImages = new List<CarImage>();

            if (files is null || files.Count == 0)
                return carImages;

            foreach (var file in files)
            {
                if (file is null)
                    continue;

                var img = new CarImage
                {
                    Title = file.FileName
                };

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

        /// <summary>
        /// Transforms Car Images to Images Models
        /// </summary>
        /// <param name="carImages"></param>
        /// <returns></returns>
        private ICollection<ImageModel> GetRetrievedImages(ICollection<CarImage> carImages)
        {
            var imageModels = new List<ImageModel>();

            if (carImages is null)
                return imageModels;

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

        /// <summary>
        /// Creates validation results
        /// </summary>
        /// <param name="error"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> CreateValidationResults(string error, string memberName)
        {
            return new List<ValidationResult> { new ValidationResult(error, new List<string> { memberName }) };
        }

        /// <summary>
        /// Validates a model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ICollection<ValidationResult> Validate(object model)
        {
            var validationResult = new List<ValidationResult>();
            Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), validationResult, true);
            return validationResult;
        }

        /// <summary>
        /// Validates a lot model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validationResult"></param>
        /// <returns></returns>
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

            if (model.SaleType == 0)
                validationResult.Add(new ValidationResult("Specify Sale Type", new List<string> { "SaleType" }));

            if (model.Car.Mileage < 0)
                validationResult.Add(new ValidationResult("Invalid Car Mileage", new List<string> { "Car.Mileage" }));

            if (model.Car.Year > DateTime.UtcNow.Year
                || model.Car.Year < 1885)
                validationResult.Add(new ValidationResult("Invalid Car Year", new List<string> { "Car.Year" }));

            if (model.Car.TechnicalPassport.BodyType == 0)
                validationResult.Add(new ValidationResult("Specify Body Type", new List<string> { "Car.TechnicalPassport.BodyType" }));

            if (model.Car.TechnicalPassport.DriveUnit == 0)
                validationResult.Add(new ValidationResult("Specify Drive Unit", new List<string> { "Car.TechnicalPassport.DriveUnit" }));

            if (model.Car.TechnicalPassport.Transmission == 0)
                validationResult.Add(new ValidationResult("Specify Transmission", new List<string> { "Car.TechnicalPassport.Transmission" }));

            return validationResult.Count == 0;
        }
    }
}
