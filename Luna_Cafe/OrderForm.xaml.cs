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
        public OrderForm()
        {
            InitializeComponent();
            ViewModel = new OrderViewModel();
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

            Dish editableDish = Dish.FromDTO(ViewModel.SelectedDish);

            DishForm form = new DishForm(editableDish);

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
    }
}
