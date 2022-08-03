using System;
using System.Collections.Generic;

namespace Ads.Models
{
    public partial class TblAd
    {
        public int AdId { get; set; }
        public string AdTitle { get; set; } = null!;
        public string AdContent { get; set; } = null!;
        public int AdGoodsPrice { get; set; }
        public int AdCost { get; set; }
        public int AdOwnerId { get; set; }

        public virtual TblAdOwner AdOwner { get; set; } = null!;
    }
}
