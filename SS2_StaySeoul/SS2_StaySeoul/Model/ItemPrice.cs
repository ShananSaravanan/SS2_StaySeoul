using System;
using System.Collections.Generic;
using System.Text;

namespace SS2_StaySeoul.Model
{
    public class ItemPrice
    {
        public long ID { get; set; }
        public string itemTitle { get; set; }
        public Guid GUID { get; set; }
        public long ItemID { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Price { get; set; }
        public long CancellationPolicyID { get; set; }
        public decimal holidayPrice { get; set; }
        public decimal weekendPrice { get; set; }
        public long normalCancellationPolicy { get; set; }
        public long weekendCancellationPolicy { get; set; }
        public long holidayCancellationPolicy { get; set; }
    }
}
