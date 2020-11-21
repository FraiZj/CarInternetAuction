using InternetAuction.BLL.Models;
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
        Task Log(ExceptionLogModel exceptionLogModel);
    }
}
