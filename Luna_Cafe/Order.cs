using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Order
    {
        
        private string cafeName;
        private DateTime date;

        private List<Dish> dishes = new List<Dish>();

        public Order(string cafeName)
        {
            if (string.IsNullOrWhiteSpace(cafeName))
                throw new ArgumentException("Назва кафе обов’язкова");

            this.cafeName = cafeName;
            this.date = DateTime.Now;
        }

       
        public Order(string cafeName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cafeName))
                throw new ArgumentException("Назва кафе обов’язкова");

            this.cafeName = cafeName;
            this.date = date; // використовуємо передану дату
        }

        public string GetCafeName() => cafeName;
        public DateTime GetDate() => date;
        public List<Dish> GetDishes() => dishes;

        public void AddDish(Dish dish)
        {
            if (dish == null) throw new ArgumentNullException(nameof(dish));
            dishes.Add(dish);
        }

        public int GetTotalTime()
        {
            return dishes.Sum(d => d.GetCookingTime());
        }

        //!!!!!!!!!!!!!
        public int GetTotalTimeAdvanced()
        {
            return dishes
                .GroupBy(d => d.GetChef().GetFirstName() + " " + d.GetChef().GetLastName())
                .Select(group => group.Sum(d => d.GetCookingTime()))
                .Max();
        }

        //public string ToShortString()
        //{
        //    return $"Кафе: {cafeName}, {date:G}, {GetTotalTime()} хв";
        //}

        //public override string ToString()
        //{
        //    var dishList = string.Join("\n", dishes.Select(d => d.ToShortString()));
        //    return $"Замовлення з {cafeName} від {date:G}\nСтрави:\n{dishList}\nОчікуваний час: {GetTotalTime()} хв";
        //}

        public OrderDTO ToDTO()
        {
            return new OrderDTO
            {
                CafeName = cafeName,
                Date = date,
                Dishes = dishes.Select(d => d.ToDTO()).ToList()
            };
        }

        public static Order FromDTO(OrderDTO dto)
        {
            var order = new Order(dto.CafeName, dto.Date);

            foreach (var dishDto in dto.Dishes)
            {
                order.AddDish(Dish.FromDTO(dishDto));
            }
            return order;
        }
    }


}

