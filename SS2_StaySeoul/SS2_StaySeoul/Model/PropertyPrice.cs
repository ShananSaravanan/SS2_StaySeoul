using System;
using System.Collections.Generic;
using System.Text;

namespace SS2_StaySeoul.Model
{
    public class PropertyPrice
    {
        public string title { get; set; }
        public long ID { get;set; }
        public string iconName { get; set; }
        public string color { get; set; }
        public decimal price { get; set; }
        public string rules { get; set; }
        public DateTime date { get; set; }
        public string dispRules => "(" + rules + ")";

        public DateTime displaydate => date.AddDays(1).Date;
        public string dateDetail => displaydate.ToShortDateString();
       
    }
}
