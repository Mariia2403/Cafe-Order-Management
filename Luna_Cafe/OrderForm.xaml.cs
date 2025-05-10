using System;
using System.ComponentModel;
using System.Windows;

namespace Luna_Cafe
{
    /// <summary>
    /// var dishForm = new DishForm();
    /// 
    //if (dishForm.ShowDialog() == true)
    //{
    //    var createdDish = dishForm.CreatedDish;
    //    Dishes.Add(createdDish); 
    // або Order.AddDish(createdDish)

    /// </summary>
    /// 
    public partial class OrderForm : Window
    {
        public OrderViewModel ViewModel { get; }
        public Order CreatedOrder { get; private set; }
        public OrderForm()
        {
            InitializeComponent();
            ViewModel = new OrderViewModel();
            DataContext = ViewModel;
        }
        public OrderForm(OrderDTO existingOrderDto)
        {
            InitializeComponent();
            ViewModel = new OrderViewModel(existingOrderDto);
            DataContext = ViewModel;
        }


        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            DishForm dishForm = new DishForm();

            if (dishForm.ShowDialog() == true)
            {
                if (dishForm.CreatedDish != null)
                {
                    DishDTO dishDTO = dishForm.CreatedDish.ToDTO();
                    ViewModel.Dishes.Add(dishDTO); // 👈 Add to ObservableCollection<DishDTO>
                }
            }
        }

        private void EditDish_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedDish == null) return;

            DishDTO editableDto = ViewModel.SelectedDish; // DTO вже у ViewModel
            DishForm form = new DishForm(editableDto); // передаємо DTO напряму

            if (form.ShowDialog() == true)
            {
                // Отримуємо оновлену страву
                var updated = form.CreatedDish.ToDTO();

                // Замінюємо в колекції
                int index = ViewModel.Dishes.IndexOf(ViewModel.SelectedDish);
                if (index >= 0)
                    ViewModel.Dishes[index] = updated;
            }
        }

        private void DeleteDish_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedDish == null) return;

            var result = MessageBox.Show("Видалити цю страву?", "Підтвердження", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ViewModel.DeleteSelectedDish();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Dishes.Count == 0)
            {
                MessageBox.Show("Замовлення повинно містити хоча б одну страву!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Order order = new Order(ViewModel.CafeName);
                foreach (DishDTO dishDto in ViewModel.Dishes)
                {
                    order.AddDish(Dish.FromDTO(dishDto));
                }

                CreatedOrder = order;
                MessageBox.Show("Замовлення успішно збережено!", ":)", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        private void CancleOrder_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // Якщо збережено вручну — нічого не питати
            if (this.DialogResult == true || this.DialogResult == false)
                return;

            // Якщо нічого не змінено — просто закрити
            if (!ViewModel.IsDirty)
                return;

            var result = MessageBox.Show(
                "Зберегти зміни перед закриттям?",
                "Підтвердження",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save_Click(this, new RoutedEventArgs()); // автоматично зберегти
                    break;

                case MessageBoxResult.No:
                    this.DialogResult = false;
                    break;

                case MessageBoxResult.Cancel:
                    e.Cancel = true; // залишити відкритим
                    break;
            }
        }

        private void CloseOrder_Click(object sender, RoutedEventArgs e)
        {
            // Якщо нічого не змінено, просто закриваємо
            if (!ViewModel.IsDirty)
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
                    Save_Click(this, new RoutedEventArgs()); 
                    if (CreatedOrder != null) 
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    break;

                case MessageBoxResult.No:
                    this.DialogResult = false;
                    this.Close();
                    break;

                case MessageBoxResult.Cancel:
                   
                    break;
            }
        }
    }
}
