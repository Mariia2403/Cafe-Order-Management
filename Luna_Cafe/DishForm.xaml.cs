using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public DishForm(Dish existingDish)
        {
            InitializeComponent();
            ViewModel = new AddDishViewModel(existingDish.ToDTO());
            DataContext = ViewModel;
        }
        private void Save_Click_1(object sender, RoutedEventArgs e)
        {

            // Перевірка імені та прізвища перед збереженням
            if (!ViewModel.IsChefNameValid(ViewModel.ChefFirstName) || !ViewModel.IsChefNameValid(ViewModel.ChefLastName))
            {
                MessageBox.Show("Перевірте ім'я та прізвище повара. Має починатися з великої літери і містити тільки літери.",
                    "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // ❌ Зупиняємо збереження
            }

            DishDTO dto = new DishDTO
            {
                DishName = ViewModel.DishName,
                Cost = double.TryParse(ViewModel.Price, out double parsedPrice) ? parsedPrice : 0,
                CookingTime = int.TryParse(ViewModel.CookingTime, out int parsedTime) ? parsedTime : 0,
                Category = ViewModel.SelectedCategory.CategoryValue.ToString(),
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            // Якщо нічого не змінено — просто закриваємо
            if (!ViewModel.IsModified)
            {
                this.DialogResult = false; 
                this.Close();
                return;
            }

            // Якщо були зміни — питаємо
            var result = MessageBox.Show(
                "Зберегти зміни перед закриттям?",
                "Підтвердження",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save_Click_1(sender, e);
                    this.DialogResult = true;
                    this.Close();
                    break;

                case MessageBoxResult.No:
                    this.DialogResult = false;
                    this.Close();
                    break;

                case MessageBoxResult.Cancel:
                  
                    break;
            }
        }
        private void ChefFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ViewModel.ChefFirstName) && !ViewModel.IsChefNameValid(ViewModel.ChefFirstName))
            {
                MessageBox.Show(
                    "Ім'я повара некоректне.\nПравильний формат: Велика літера, тільки букви.",
                    "Помилка введення",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        private void ChefLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ViewModel.ChefLastName) && !ViewModel.IsChefNameValid(ViewModel.ChefLastName))
            {
                MessageBox.Show(
                    "Прізвище повара некоректне.\nПравильний формат: Велика літера, тільки букви.",
                    "Помилка введення",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

      

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Якщо нічого не змінилось — не питати
            if (!ViewModel.IsModified)
                return;

            // Якщо вже явно закрили — не питати повторно
            if (this.DialogResult == true || this.DialogResult == false)
                return;

            // Питаємо користувача
            var result = MessageBox.Show(
                "Зберегти зміни перед закриттям?",
                "Підтвердження",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = true;

               
                this.Dispatcher.InvokeAsync(() =>
                {
                    Save_Click_1(sender, new RoutedEventArgs());
                });
            }

            if (result == MessageBoxResult.No)
            {
                this.DialogResult = false;
            }
        }

    }
}

