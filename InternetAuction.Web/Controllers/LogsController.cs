using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using InternetAuction.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    /// <summary>
    /// Represents the logs controller class
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    public class LogsController : Controller
    {
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Initializes an instance of the logs controller with logger service
        /// </summary>
        /// <param name="loggerService"></param>
        public LogsController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        /// <summary>
        /// Returns view with all logs
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Logs(int page = 1)
        {
            var logs = _loggerService.GetAll().OrderByDescending(l => l.DateTime);
            var logViewModel = CreateLotViewModel(logs, page);
            return View(logViewModel);
        }

        /// <summary>
        /// Creates lot view model
        /// </summary>
        /// <param name="logs"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private LogViewModel CreateLotViewModel(IEnumerable<ExceptionLogModel> logs, int page)
        {
            var pageSize = 6;

            if (Math.Ceiling((double)logs.Count() / pageSize) < page || page < 1)
                page = 1;

            var logsPerPage = logs.Skip((page - 1) * pageSize).Take(pageSize);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = logs.Count()
            };

            return new LogViewModel
            {
                Logs = logsPerPage,
                PageInfo = pageInfo
            };
        }
    }
}