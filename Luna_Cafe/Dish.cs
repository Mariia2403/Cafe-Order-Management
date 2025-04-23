using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Dish
    {
       
        private string name;
        private double cost;
        private int cookingTime;
        private Category.FoodCategories category;
        private Chef chef;

        // Конструктор з перевірками
        public Dish(string name, double cost, int time, Category.FoodCategories category, Chef chef)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва не може бути порожня");
            if (cost <= 0) throw new ArgumentException("Ціна має бути більшою за 0");
            if (time <= 0) throw new ArgumentException("Час приготування має бути більшим за 0");

            this.name = name;
            this.cost = cost;
            this.cookingTime = time;
            this.category = category;
            this.chef = chef ?? throw new ArgumentNullException(nameof(chef));
        }

        // Публічні методи для доступу до полів (інкапсуляція)
        public string GetName() => name;
        public double GetCost() => cost;
        public int GetCookingTime() => cookingTime;
        public Category.FoodCategories GetCategory() => category;
        public Chef GetChef() => chef;

        public string ToShortString()
        {
            return $"{name} ({category}) – {cost} грн, готує {chef.FullName()}, {cookingTime} хв";
        }

        // Перетворення в DTO
        public DishDTO ToDTO()
        {
            return new DishDTO
            {
                DishName = name,
                Cost = cost,
                CookingTime = cookingTime,
                Category = category.ToString(),
                Chef = new ChefDTO
                {
                    FirstName = chef.GetFirstName(),
                    LastName = chef.GetLastName()
                }
            };
        }

        // Відновлення з DTO
        public static Dish FromDTO(DishDTO dto)
        {
            //!!!!!
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
