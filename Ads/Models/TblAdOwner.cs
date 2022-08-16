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

        public virtual  ICollection<TblAd> TblAds { get; set; }

        public void SetAdOwner(AdOwner adOwner)
        {
            OwnIsSub = adOwner.OwnIsSub;
            OwnSubId = adOwner.OwnSubId;
            OwnCompanyOrgNr = adOwner.OwnCompanyOrgNr;
            OwnName = adOwner.OwnName;
            OwnPhone = adOwner.OwnPhone;
            OwnDeliveryAdress = adOwner.OwnDeliveryAdress;
            OwnDeliveryCounty = adOwner.OwnDeliveryCounty;
            OwnDeliveryZip = adOwner.OwnDeliveryZip;
            OwnBillingAdress = adOwner.OwnBillingAdress;
            OwnBillingCounty = adOwner.OwnBillingCounty;
            OwnBillingZip = adOwner.OwnBillingZip;
        }

        public AdOwner GetAdOwner()
        {
            AdOwner adOwner = new AdOwner();
            adOwner.OwnId = OwnId;
            adOwner.OwnIsSub = OwnIsSub;
            adOwner.OwnSubId = OwnSubId;
            adOwner.OwnCompanyOrgNr = OwnCompanyOrgNr;
            adOwner.OwnName = OwnName;
            adOwner.OwnPhone = OwnPhone;
            adOwner.OwnDeliveryAdress = OwnDeliveryAdress;
            adOwner.OwnDeliveryZip = OwnDeliveryZip;
            adOwner.OwnDeliveryCounty = OwnDeliveryCounty;
            adOwner.OwnBillingAdress = OwnBillingAdress;
            adOwner.OwnBillingZip = OwnBillingZip;
            adOwner.OwnBillingCounty = OwnBillingCounty;
            return adOwner;
        }

        public void setCompany(Company company)
        {
            OwnIsSub = false;
            OwnCompanyOrgNr = company.CompanyOrgNr;
            OwnName = company.Name;
            OwnPhone = company.Phone;
            OwnDeliveryAdress = company.DeliveryAdress;
            OwnDeliveryCounty = company.DeliveryCounty;
            OwnDeliveryZip = company.DeliveryZip;
            OwnBillingAdress = company.BillingAdress;
            OwnBillingCounty = company.BillingCounty;
            OwnBillingZip = company.BillingZip;
        }

        public void setSubscriber(Subscriber sub)
        {
            OwnIsSub = true;
            OwnSubId = sub.SubId;
            OwnName = sub.SubName;
            OwnPhone = sub.SubPhone;
            OwnDeliveryAdress = sub.SubDeliveryAdress;
            OwnDeliveryCounty = sub.SubDeliveryCounty;
            OwnDeliveryZip = sub.SubDeliveryZip;
        }

        public Company GetCompany()
        {
            Company company = new Company();
            company.CompanyOrgNr = OwnCompanyOrgNr;
            company.Name = OwnName;
            company.Phone = OwnPhone;
            company.DeliveryAdress = OwnDeliveryAdress;
            company.DeliveryCounty = OwnDeliveryCounty;
            company.DeliveryZip = OwnDeliveryZip;
            company.BillingAdress = OwnBillingAdress;
            company.BillingCounty = OwnBillingCounty;
            company.BillingZip = OwnBillingZip;

            return company;
        }

        public Subscriber GetSubscriber()
        {
            Subscriber sub = new Subscriber();
            sub.SubId = (int)OwnSubId;
            sub.SubName = OwnName;
            sub.SubPhone = OwnPhone;
            sub.SubDeliveryAdress = OwnDeliveryAdress;
            sub.SubDeliveryZip = OwnDeliveryZip;
            sub.SubDeliveryCounty = OwnDeliveryCounty;
            return sub;
        }
    }
}
