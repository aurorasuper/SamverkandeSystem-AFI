using System;
using System.ComponentModel.DataAnnotations;

namespace Ads.Models
{
    public class Company
    {
        [Required]
        public string CompanyOrgNr { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(13)]
        [RegularExpression("^[0-9]*$")]
        public string Phone { get; set; } = null!;
        [Required]
        public string DeliveryAdress { get; set; } = null!;
        [Required]
        [StringLength(maximumLength:5,MinimumLength =5)]
        [RegularExpression("^[0-9]*$")]
        public string DeliveryZip { get; set; } = null!;
        [Required]
        public string DeliveryCounty { get; set; } = null!;
        [Required]
        public string BillingAdress { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 5)]
        [RegularExpression("^[0-9]*$")]
        public string BillingZip { get; set; } = null!;
        [Required]
        public string BillingCounty { get; set; } = null!;
        public Company()
        {
        }
    }
}

