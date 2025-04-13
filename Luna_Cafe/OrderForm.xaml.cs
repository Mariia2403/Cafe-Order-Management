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
        public AddDishViewModel ViewModel { get; }
        public OrderForm()
        {
            InitializeComponent();
            ViewModel = new AddDishViewModel();
            DataContext = ViewModel;
        }
    }
}
