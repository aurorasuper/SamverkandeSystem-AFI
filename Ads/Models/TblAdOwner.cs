using System;
using System.Collections.Generic;

namespace Ads.Models
{
    public partial class TblAdOwner
    {
        public TblAdOwner()
        {
            TblAds = new HashSet<TblAd>();
        }

        public int OwnId { get; set; }
        public bool? OwnIsSub { get; set; }
        public int? OwnSubId { get; set; }
        public string? OwnCompanyOrgNr { get; set; }
        public string OwnName { get; set; } = null!;
        public string OwnPhone { get; set; } = null!;
        public string OwnDeliveryAdress { get; set; } = null!;
        public string OwnDeliveryZip { get; set; } = null!;
        public string OwnDeliveryCounty { get; set; } = null!;
        public string? OwnBillingAdress { get; set; }
        public string? OwnBillingZip { get; set; }
        public string? OwnBillingCounty { get; set; }

        public virtual ICollection<TblAd> TblAds { get; set; }
    }
}
