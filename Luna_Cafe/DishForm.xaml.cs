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
using System.Windows.Shapes;

namespace Luna_Cafe
{
    /// <summary>
    /// Interaction logic for DishForm.xaml
    /// </summary>
    public partial class DishForm : Window
    {
        public AddDishViewModel ViewModel { get; }
        public Dish CreatedDish { get; private set; }
        public DishForm()
        {
            InitializeComponent();
            ViewModel = new AddDishViewModel();
            DataContext = ViewModel;
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            DishDTO dto = new DishDTO
            {
                DishName = ViewModel.DishName,
                Cost = int.TryParse(ViewModel.Price,out int parsedPrice) ? parsedPrice : 0,
                CookingTime = int.TryParse(ViewModel.CookingTime, out int parsedTime) ? parsedTime : 0,
                Category = ViewModel.SelectedCategory?.DishCategory ?? "",
                Chef = new ChefDTO
                {
                    FirstName = ViewModel.ChefFirstName,
                    LastName = ViewModel.ChefLastName
                }

            };


            try
            {
                // Перетворюємо DTO на модель
                CreatedDish = Dish.FromDTO(dto);

                // Закриваємо вікно і передаємо результат
                this.DialogResult = true;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при створенні страви: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Зберегти зміни перед закриттям?",
                "Підтвердження",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save_Click_1(sender, e); // Ти вже все зробила тут — переюзуємо
                    break;

                case MessageBoxResult.No:
                    this.DialogResult = false;
                    this.Close();
                    break;

                case MessageBoxResult.Cancel:
                    // Нічого не робити — хай вікно лишається відкритим
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DialogResult == true || this.DialogResult == false)
            {
                // Уже було закриття з кнопки — не питати повторно
                return;
            }

            var result = MessageBox.Show(
                "Зберегти зміни перед закриттям?",
                "Підтвердження",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save_Click_1(sender, new RoutedEventArgs());
                    break;

                case MessageBoxResult.No:
                    this.DialogResult = false;
                    break;

                case MessageBoxResult.Cancel:
                    e.Cancel = true; // скасовуємо закриття
                    break;
            }
        }

    }
}

