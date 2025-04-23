using System.Collections.Generic;
using static Luna_Cafe.Category;

namespace Luna_Cafe
{
    public class FoodCategoryTranslator
    {
        public static readonly Dictionary<FoodCategories, string> ToUkrainian = new Dictionary<FoodCategories, string>
    {
        { FoodCategories.Cold_Appetisers, "Холодні закуски" },
        { FoodCategories.First_Courses, "Перші страви" },
        { FoodCategories.Second_Courses, "Другі страви" },
        { FoodCategories.Desserts, "Десерти" },
        { FoodCategories.Drinks, "Напої" }
    };
    }
}
