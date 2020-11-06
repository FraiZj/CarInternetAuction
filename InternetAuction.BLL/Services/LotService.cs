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

        private bool Validate(LotModel model, out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), validationResults, true);
        }

        public async Task<OperationDetails> AddAsync(LotModel model)
        {
            if (model is null) 
                return new OperationDetails(false, new List<string> { "Lot cannot be null" });

            if (!Validate(model, out var validationResults))
                return new OperationDetails(false, validationResults.Select(r => r.ErrorMessage));

            if (model.AuctionDate.ToUniversalTime() < DateTime.UtcNow
                || DateTime.UtcNow.Month - model.AuctionDate.ToUniversalTime().Month > 1)
                return new OperationDetails(false, new List<string> { "Invalid Auction Date" });

            try
            {
                var lot = _mapper.Map<Lot>(model);
                _unitOfWork.LotRepository.Add(lot);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occure while adding a new lot", ex.InnerException);
            }
            
            return new OperationDetails(true);
        }

        public Task<OperationDetails> AddToArchiveAsync(LotModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetails> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LotModel> GetActiveLotByIdWIthDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LotModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<LotModel> GetAllActiveLots()
        {
            throw new NotImplementedException();
        }

        public IQueryable<LotModel> GetAllActiveLotsWithDetails()
        {
            throw new NotImplementedException();
        }

        public IQueryable<LotModel> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<LotModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LotModel> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetails> SellLotAsync(LotModel model, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetails> UpdateAsync(LotModel model)
        {
            throw new NotImplementedException();
        }
    }
}
