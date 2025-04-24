using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Luna_Cafe
{
    public static class DataStorage
    {
        private static readonly string filePath = "orders.json";

        // Зберігає список замовлень у файл
        public static void Save(List<OrderDTO> orders)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(orders, options);
            File.WriteAllText(filePath, json);
        }

        // Завантажує список замовлень із файлу
        public static List<OrderDTO> Load()
        {
            if (!File.Exists(filePath))
                return new List<OrderDTO>(); // Якщо немає файлу — повертаємо порожній список

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<OrderDTO>>(json) ?? new List<OrderDTO>();
        }
    }
}
