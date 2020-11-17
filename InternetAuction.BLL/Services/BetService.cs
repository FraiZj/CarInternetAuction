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
    public class BetService : IBetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool isDisposed;

        public BetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationDetails> AddAsync(BetModel model)
        {
            try
            {
                if (!ValidateBetModel(model, out var validationResults))
                    return new OperationDetails(false, validationResults);

                var lot = await _unitOfWork.LotRepository.GetByIdAsync(model.LotId);

                if (lot is null) 
                    return new OperationDetails(false, CreateValidationResults($"Lot with Id = {model.LotId} does not exist", "Sum"));

                if (lot.SellerId == model.UserId) 
                    return new OperationDetails(false, CreateValidationResults($"User cannot place be on his own lot", "Sum"));

                var currentBet = lot.Bets.Count != 0 ?
                    lot.Bets.Max(b => b.Sum)
                    : lot.StartPrice;

                if (model.Sum <= currentBet)
                    return new OperationDetails(false, CreateValidationResults($"Sum cannot be equal or less than current bet", "Sum"));

                var bet = _mapper.Map<Bet>(model);
                _unitOfWork.BetRepository.Add(bet);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while adding a new bet", ex.InnerException);
            }
        }

        public async Task<OperationDetails> DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.BetRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while deleting a bet", ex.InnerException);
            }
        }

        public IQueryable<BetModel> GetAll()
        {
            try
            {
                var bets = _unitOfWork.BetRepository.FindAllWithDetails();
                return _mapper.Map<IQueryable<BetModel>>(bets);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching bets", ex.InnerException);
            }
        }

        public async Task<BetModel> GetByIdAsync(int id)
        {
            try
            {
                var bet = await _unitOfWork.BetRepository.GetByIdWithDetailsAsync(id);
                return _mapper.Map<BetModel>(bet);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching a bet", ex.InnerException);
            }
        }

        public async Task<OperationDetails> UpdateAsync(BetModel model)
        {
            try
            {
                if (!ValidateBetModel(model, out var validationResults))
                    return new OperationDetails(false, validationResults);

                var bet = _mapper.Map<Bet>(model);
                _unitOfWork.BetRepository.Update(bet);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while updating a bet", ex.InnerException);
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

        private IEnumerable<ValidationResult> CreateValidationResults(string error, string memberName)
        {
            return new List<ValidationResult> { new ValidationResult(error, new List<string> { memberName }) };
        }

        private ICollection<ValidationResult> Validate(object model)
        {
            var validationResult = new List<ValidationResult>();
            Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), validationResult, true);
            return validationResult;
        }

        private bool ValidateBetModel(BetModel model, out ICollection<ValidationResult> validationResult)
        {
            if (model is null)
            {
                validationResult = new List<ValidationResult> { new ValidationResult("BetModel cannot be null") };
                return false;
            }

            var modelValidationResult = Validate(model);

            validationResult = modelValidationResult.ToList();

            return validationResult.Count == 0;
        }
    }
}
