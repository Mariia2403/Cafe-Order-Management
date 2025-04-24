using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;

namespace Luna_Cafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
           // DataContext = ViewModel;

            // Завантаження збережених замовлень при запуску
            var savedOrders = DataStorage.Load();

            foreach (var dto in savedOrders)
            {
                var order = Order.FromDTO(dto);
                ViewModel.AddOrder(order);
            }

            DataContext = ViewModel;
        }
         // Збереження замовлень при закритті вікна
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Конвертуємо в DTO для збереження
            var orderDTOs = ViewModel.Orders.Select(o => o.Order.ToDTO()).ToList();
            DataStorage.Save(orderDTOs);

            base.OnClosing(e);
        }

       
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var form = new OrderForm();
            bool? result = form.ShowDialog();

            if (result == true && form.CreatedOrder != null)
            {
                ViewModel.AddOrder(form.CreatedOrder);
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedOrder == null)
                return;

            // Отримуємо оригінальний Order
            var orderToEdit = ViewModel.SelectedOrder.Order;

            // Перетворюємо в DTO, передаємо у форму
            var dto = orderToEdit.ToDTO();

            var form = new OrderForm(dto); // Припустимо, OrderForm має конструктор з OrderDTO

            if (form.ShowDialog() == true)
            {
                // Користувач натиснув "Зберегти", отже отримуємо змінене замовлення
                var editedOrder = form.CreatedOrder;

                // Видаляємо старе і додаємо нове (або оновлюємо)
                ViewModel.Orders.Remove(ViewModel.SelectedOrder);
                ViewModel.AddOrder(editedOrder);
            }
        }
    }
}
