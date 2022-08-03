using System;
using System.Collections.Generic;

namespace Subscribers.Models
{
    public partial class TblSubscriber
    {
        public int SubId { get; set; }
        public string SubName { get; set; } = null!;
        public string SubPhone { get; set; } = null!;
        public string SubDeliveryAdress { get; set; } = null!;
        public string SubDeliveryZip { get; set; } = null!;
        public string SubDeliveryCounty { get; set; } = null!;
    }
}
