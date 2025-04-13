using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class OrderDTO
    {
        public string CafeName { get; set; }
        public DateTime Date { get; set; }
        public List<DishDTO> Dishes { get; set; }
    }
}
