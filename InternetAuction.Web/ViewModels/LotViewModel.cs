using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using System.Collections.Generic;

namespace InternetAuction.Web.ViewModels
{
    public class LotViewModel
    {
        public IEnumerable<LotModel> Lots { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}