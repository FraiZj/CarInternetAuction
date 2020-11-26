using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using System.Collections.Generic;

namespace InternetAuction.Web.ViewModels
{
    public class LogViewModel
    {
        public IEnumerable<ExceptionLogModel> Logs { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}