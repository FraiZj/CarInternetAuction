using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Services;
using System;
using System.Web.Mvc;

namespace InternetAuction.Web.Infrastructure
{
    /// <summary>
    /// Represents an exception logger attribute
    /// </summary>
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private ILoggerService LoggerService => DependencyResolver.Current.GetService<LoggerService>();

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            var exceptionLog = new ExceptionLogModel
            {
                ExceptionMessage = filterContext.Exception.Message,
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                Action = filterContext.RouteData.Values["action"].ToString(),
                IP = filterContext.HttpContext.Request.UserHostAddress,
                DateTime = DateTime.UtcNow
            };

            LoggerService.Log(exceptionLog);

            filterContext.Result = new RedirectResult("~/Errors/InternalError");
            filterContext.ExceptionHandled = true;
        }
    }
}