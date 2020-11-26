using InternetAuction.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Interface that exposes logger service methods
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs an exception
        /// </summary>
        Task LogAsync(ExceptionLogModel exceptionLogModel);

        /// <summary>
        /// Returns all logs
        /// </summary>
        /// <returns></returns>
        IEnumerable<ExceptionLogModel> GetAll();
    }
}
