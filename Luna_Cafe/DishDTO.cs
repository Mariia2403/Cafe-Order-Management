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
        public double Cost { get; set; }
        public int CookingTime { get; set; }
        public string Category { get; set; }
        public ChefDTO Chef { get; set; }

        public string CategoryDisplay
        {
            get
            {
                if (Enum.TryParse<Category.FoodCategories>(this.Category, out var parsedEnum))
                {
                    return FoodCategoryTranslator.ToUkrainian.TryGetValue(parsedEnum, out string translated)
                        ? translated
                        : "Невідомо";
                }
                return "Невідома категорія";
            }
        }

        public string CookingTimeDisplay
        {
            get
            {
                int totalMinutes = CookingTime;

                int hours = totalMinutes / 60;
                int minutes = totalMinutes % 60;

                if (hours > 0 && minutes > 0)
                    return $"{hours} год {minutes} хв";
                else if (hours > 0)
                    return $"{hours} год";
                else
                    return $"{minutes} хв";
            }
        }
    }
}
