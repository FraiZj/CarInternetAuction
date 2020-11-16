﻿using InternetAuction.BLL.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InternetAuction.BLL.Models
{
    public class CarModel : BaseModel
    {
        [Required, RegularExpression(@"^[a-zA-Z-_ ]+$")]
        public string Brand { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z0-9-_ ]+$")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required, Display(Name = "Engine Type")]
        public string EngineType { get; set; }

        [Required, Display(Name = "Technical Passport")]
        public TechnicalPassportModel TechnicalPassport { get; set; }

        [Display(Name = "Car Images")]
        public ICollection<ImageModel> CarImages { get; set; }

        [Display(Name = "Files")]
        public ICollection<HttpPostedFileBase> Files { get; set; }
    }
}
