using System;
namespace Ads.Models
{
    public class Ad
    {
        public int AdId { get; set; }
        public string AdTitle { get; set; }
        public string AdContent { get; set; }
        public int AdGoodsPrice { get; set; }
        public int AdCost { get; set; }
        public int AdOwnerId { get; set; }

        public Ad()
        {
        }

    }
}

