using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Luna_Cafe
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderSummaryDTO> Orders { get; set; } = new ObservableCollection<OrderSummaryDTO>();


        public void AddOrder(Order order)
        {
            Orders.Add(new OrderSummaryDTO
            {
                CafeName = order.GetCafeName(),
                Date = order.GetDate().ToString("g"),
                TotalSum = order.GetDishes().Sum(d => d.GetCost()) + " грн",
                CookingTimeDisplay = FormatTime(order.GetTotalTimeAdvanced())
            });
        }

        private string FormatTime(int minutes)
        {
            int h = minutes / 60;
            int m = minutes % 60;
            if (h > 0 && m > 0) return $"{h} год {m} хв";
            if (h > 0) return $"{h} год";
            return $"{m} хв";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
