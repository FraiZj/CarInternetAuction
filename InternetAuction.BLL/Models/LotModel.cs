﻿using InternetAuction.BLL.EnumsDtos;
using InternetAuction.BLL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetAuction.BLL.Models
{
    public class LotModel : BaseModel
    {
        [Required, Display(Name = "Car")]
        public CarModel Car { get; set; }

        [Required, Display(Name = "Seller Id")]
        public string SellerId { get; set; }

        [Display(Name = "Buyer Id")]
        public string BuyerId { get; set; }

        [Required, Display(Name = "Sale Type")]
        public SaleTypeDto SaleType { get; set; }

        [Required, Display(Name = "Auction Date")]
        public DateTime AuctionDate { get; set; }

        public ICollection<int> Bets { get; set; }
    }
}