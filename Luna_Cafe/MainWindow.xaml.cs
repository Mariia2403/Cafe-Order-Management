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
        private bool isSaved = true;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();

            var savedOrders = DataStorage.Load(); 
            var errors = new List<string>();

            foreach (var dto in savedOrders)
            {
                try
                {
                    var order = Order.FromDTO(dto); 
                    ViewModel.AddOrder(order);
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message); 
                }
            }

            if (errors.Any())
            {
                MessageBox.Show(
                    "Стались помилки при завантаженні:\n" + string.Join("\n", errors),
                    "Помилки даних",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }

            DataContext = ViewModel;
        }
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved)
            {
                var result = MessageBox.Show(
                    "Замовлення не збережені. Зберегти перед виходом?",
                    "Підтвердження збереження",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    var orderDTOs = ViewModel.Orders.Select(o => o.Order.ToDTO()).ToList();
                    DataStorage.Save(orderDTOs);
                }
                // якщо No — просто закриваємо
            }

            base.OnClosing(e);
        }

       
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var form = new OrderForm();
            bool? result = form.ShowDialog();

            if (result == true && form.CreatedOrder != null)
            {
                ViewModel.AddOrder(form.CreatedOrder);
                isSaved = false;
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedOrder == null)
                return;

           
            var orderToEdit = ViewModel.SelectedOrder.Order;

            // Перетворюємо в DTO, передаємо у форму
            var dto = orderToEdit.ToDTO();

            var form = new OrderForm(dto); 

            if (form.ShowDialog() == true)
            {
                // Користувач натиснув "Зберегти", отже отримуємо змінене замовлення
                var editedOrder = form.CreatedOrder;

                // Видаляємо старе і додаємо нове
                ViewModel.Orders.Remove(ViewModel.SelectedOrder);
                ViewModel.AddOrder(editedOrder);
                isSaved = false;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedOrder == null)
                return;

            var result = MessageBox.Show(
                "Ви дійсно хочете видалити це замовлення?",
                "Підтвердження видалення",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                ViewModel.Orders.Remove(ViewModel.SelectedOrder);
                ViewModel.SelectedOrder = null;
                isSaved = false;
            }
        }

        private void SaveOrders_Click(object sender, RoutedEventArgs e)
        {
            // Конвертуємо замовлення в список DTO
            var orderDTOs = ViewModel.Orders.Select(o => o.Order.ToDTO()).ToList();

            //  Використовуємо DataStorage для збереження
            DataStorage.Save(orderDTOs);
            isSaved = true;
            // Повідомляємо користувача
            MessageBox.Show("Замовлення успішно збережені!", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
