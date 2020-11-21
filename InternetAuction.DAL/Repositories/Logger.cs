using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Repositories
{
    /// <summary>
    /// Represents a logger class
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes an instance of logger with context
        /// </summary>
        /// <param name="context"></param>
        public Logger(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Logs an exception to database
        /// </summary>
        /// <param name="exceptionLog"></param>
        public void Log(ExceptionLog exceptionLog)
        {
            _context.ExceptionLogs.Add(exceptionLog);
        }
    }
}
