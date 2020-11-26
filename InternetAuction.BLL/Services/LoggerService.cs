using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Validation;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Services
{
    /// <summary>
    /// Represents logger service class
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes logger service with unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public LoggerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ExceptionLogModel> GetAll()
        {
            try
            {
                var logs = _unitOfWork.Logger.GetAll();
                return _mapper.Map<IEnumerable<ExceptionLogModel>>(logs);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while getting all logs", ex);
            }
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        public async Task LogAsync(ExceptionLogModel exceptionLogModel)
        {
            try
            {
                var excepitonLog = _mapper.Map<ExceptionLog>(exceptionLogModel);
                _unitOfWork.Logger.Log(excepitonLog);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while logging an exception", ex);
            }
        }
    }
}
