using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Luna_Cafe
{
    public class AddDishViewModel : INotifyPropertyChanged
    {
        //Схоже нва List
        public ObservableCollection<OrderOption> Category { get; set; }
       


        //Дані що були введені 

      
      //  private string 

        //Заповнює комбобокс при створенні форми 
        public AddDishViewModel()
        {

            Category = new ObservableCollection<OrderOption>
            {
                new OrderOption { DishCategory = "Холодні закуски" },
                new OrderOption { DishCategory = "Перші страви" },
                new OrderOption { DishCategory = "Другі страви" },
                new OrderOption { DishCategory = "Десерти" },
                new OrderOption { DishCategory = "Напої" }
            };

         

        }

        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        private OrderOption selectedCategory;
        public OrderOption SelectedCategory
        {
            get => selectedCategory;

            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        //////////////////////////////////////////////

        private string dishName;
        public string DishName
        {
            get => dishName;
            set { dishName = value; OnPropertyChanged(nameof(DishName)); }

            // Можна додати Перевірка валідності вводу одразу при зміні
        }

        private string price;
        public string Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string cookingTime;
        public string CookingTime
        {
            get => cookingTime;
            set { cookingTime = value; OnPropertyChanged(nameof(CookingTime)); }
        }

        private string chefFirstName;
        public string ChefFirstName
        {
            get => chefFirstName;
            set { chefFirstName = value; OnPropertyChanged(nameof(ChefFirstName)); }
        }

        private string chefLastName;
        public string ChefLastName
        {
            get => chefLastName;
            set { chefLastName = value; OnPropertyChanged(nameof(ChefLastName)); }
        }

        //////////////////////////////////////////////

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        }


    }
}
