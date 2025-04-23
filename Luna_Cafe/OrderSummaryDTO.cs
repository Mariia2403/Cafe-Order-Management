using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class OrderSummaryDTO
    {
        public string CafeName { get; set; }
        public string Date { get; set; }
        public string TotalSum { get; set; }
        public string CookingTimeDisplay { get; set; }
    }
}
