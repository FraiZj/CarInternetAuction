using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using System.Collections.Generic;

namespace InternetAuction.Web.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserModel> Users { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}