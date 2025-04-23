using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class OrderOption
    {
        public Category.FoodCategories CategoryValue { get; set; } // логіка
        public string DisplayName { get; set; } // те, що бачить користувач
    }
}
