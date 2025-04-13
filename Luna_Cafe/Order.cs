using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Order
    {
        public string CafeName { get; }
        public DateTime Date { get; }
        public List<Dish> Dishes { get; } = new List<Dish>();


        // Конструктор — створює нове замовлення і одразу ставить дату
        public Order(string cafeName)
        {
            if (string.IsNullOrWhiteSpace(cafeName))
                throw new ArgumentException("Назва кафе обов’язкова");

            CafeName = cafeName;
            Date = DateTime.Now;
        }

        // Метод для додавання страви до замовлення
        public void AddDish(Dish dish)
        {
            if (dish == null) throw new ArgumentNullException(nameof(dish));
            Dishes.Add(dish);
        }

        // Обчислює загальний час приготування всіх страв
        public int GetTotalTime()
        {
            return Dishes.Sum(d => d.CookingTime);
        }

        // Повертає короткий опис замовлення: кафе, дата і загальний час
        public string ToShortString()
        {
            return $"Кафе: {CafeName}, {Date:G}, {GetTotalTime()} хв";
        }

        // Перетворює Order на DTO-об'єкт для збереження або передачі
        public OrderDTO ToDTO()
        {
            return new OrderDTO
            {
                CafeName = this.CafeName,
                Date = this.Date,
                Dishes = this.Dishes.Select(d => d.ToDTO()).ToList()
            };
        }

        // Створює Order із DTO-об'єкта (наприклад, при завантаженні з JSON)
        public static Order FromDTO(OrderDTO dto)
        {
            var order = new Order(dto.CafeName)
            {
                // Перевизначаємо дату, якщо треба
            };

            // Додаємо кожну страву з DTO до замовлення
            foreach (var dishDto in dto.Dishes)
            {
                order.AddDish(Dish.FromDTO(dishDto));
            }

            return order;
        }
    }


}

