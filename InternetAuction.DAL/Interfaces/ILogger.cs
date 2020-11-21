using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Interface that exposes logger methods
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an exception to database
        /// </summary>
        /// <param name="exceptionLog"></param>
        void Log(ExceptionLog exceptionLog);
    }
}
