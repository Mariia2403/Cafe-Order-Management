using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Dish
    {
        public string DishName { get; }
        public int Cost { get; }
        public int CookingTime { get; }
        public Category.FoodCategories Category { get; }
        public Chef Chef { get; }

        public Dish(string name, int cost, int time, Category.FoodCategories category, Chef chef)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва не може бути порожня");
            if (cost <= 0) throw new ArgumentException("Ціна має бути більшою за 0");
            if (time <= 0) throw new ArgumentException("Час приготування має бути більшим за 0");

            DishName = name;
            Cost = cost;
            CookingTime = time;
            Category = category;
            Chef = chef ?? throw new ArgumentNullException(nameof(chef));
        }

       

        public DishDTO ToDTO()
        {
            return new DishDTO
            {
                DishName = this.DishName,
                Cost = this.Cost,
                CookingTime = this.CookingTime,
                Category = this.Category.ToString(),
                Chef = new ChefDTO
                {
                    FirstName = this.Chef.FirstName,
                    LastName = this.Chef.LastName
                }
            };
        }

        public static Dish FromDTO(DishDTO dto)
        {
            if (!Enum.TryParse(dto.Category, out Category.FoodCategories parsedCategory))
                throw new ArgumentException("Невідома категорія");

            return new Dish(
                dto.DishName,
                dto.Cost,
                dto.CookingTime,
                parsedCategory,
                new Chef(dto.Chef.FirstName, dto.Chef.LastName)
            );
        }

    }
}
