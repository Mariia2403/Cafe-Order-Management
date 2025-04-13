using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class DishDTO
    {
        public string DishName { get; set; }
        public int Cost { get; set; }
        public int CookingTime { get; set; }
        public string Category { get; set; }
        public ChefDTO Chef { get; set; }
    }
}
