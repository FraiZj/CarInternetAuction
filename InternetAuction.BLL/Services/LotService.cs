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
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<OperationDetails> AddAsync(LotModel model)
        {
            try
            {
                if (!ValidateLotModel(model, out var validationResult))
                    return new OperationDetails(false, validationResult);

                var lot = _mapper.Map<Lot>(model);
                _unitOfWork.LotRepository.Add(lot);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
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
                var lots = _unitOfWork.LotRepository.FindAll();
                return _mapper.Map<IQueryable<LotModel>>(lots);
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
                var lots = _unitOfWork.LotRepository.FindAll().Where(l => l.IsActive);
                return _mapper.Map<IQueryable<LotModel>>(lots);
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
                return _mapper.Map<LotModel>(lot);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching the lot", ex.InnerException);
            }
        }

        public async Task<OperationDetails> SellLotAsync(int lotId, string userId)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdAsync(lotId);
                lot.BuyerId = userId;
                lot.IsActive = false;
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while updating lot", ex.InnerException);
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
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while updating lot", ex.InnerException);
            }
        }

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

            if (model.AuctionDate.Date < DateTime.UtcNow.Date
                    || DateTime.UtcNow.Month - model.AuctionDate.ToUniversalTime().Month > 1)
                validationResult.Add(new ValidationResult("Invalid Auction Date", new List<string> { "AuctionDate" }));

            if (model.Car.Year > DateTime.UtcNow.Year)
                validationResult.Add(new ValidationResult("Invalid Car Year", new List<string> { "Car", "Year" }));

            return validationResult.Count == 0;
        }
    }
}
