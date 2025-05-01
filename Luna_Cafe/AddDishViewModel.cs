using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Luna_Cafe
{
    public class AddDishViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<OrderOption> DishCategory { get; set; }


        public AddDishViewModel()
        {

            DishCategory = new ObservableCollection<OrderOption>
            {
                new OrderOption { CategoryValue = Luna_Cafe.Category.FoodCategories.Cold_Appetisers, DisplayName = "Холодні закуски" },
                new OrderOption { CategoryValue = Luna_Cafe.Category.FoodCategories.First_Courses, DisplayName = "Перші страви" },
                new OrderOption { CategoryValue = Luna_Cafe.Category.FoodCategories.Second_Courses, DisplayName = "Другі страви" },
                new OrderOption { CategoryValue = Luna_Cafe.Category.FoodCategories.Desserts, DisplayName = "Десерти" },
                new OrderOption { CategoryValue = Luna_Cafe.Category.FoodCategories.Drinks, DisplayName = "Напої" }
            };

        }

        public AddDishViewModel(DishDTO existingDish) : this() 
        {
            DishName = existingDish.DishName;
            Price = existingDish.Cost.ToString();
            CookingTime = existingDish.CookingTime.ToString();

          
            SelectedCategory = DishCategory.FirstOrDefault(c => c.CategoryValue.ToString() == existingDish.Category);

            ChefFirstName = existingDish.Chef.FirstName;
            ChefLastName = existingDish.Chef.LastName;

            IsModified = false;
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
        private bool isModified = false;
        public bool IsModified
        {
            get => isModified;
            set
            {
                isModified = value;
                OnPropertyChanged(nameof(IsModified));
            }
        }
        //////////////////////////////////////////////
        private string dishName;
        public string DishName
        {
            get => dishName;
            set { dishName = value;
                IsModified = true;
                OnPropertyChanged(nameof(DishName)); }

            // Можна додати Перевірка валідності вводу одразу при зміні
        }

        private string price;
        public string Price
        {
            get => price;
            set { price = value;
                IsModified = true;
                OnPropertyChanged(nameof(Price)); }
        }

        private string cookingTime;
        public string CookingTime
        {
            get => cookingTime;
            set { cookingTime = value;
                IsModified = true;
                OnPropertyChanged(nameof(CookingTime)); }
        }

        private string chefFirstName;
        public string ChefFirstName
        {
            get => chefFirstName;
            set { chefFirstName = value;
                IsModified = true;
                OnPropertyChanged(nameof(ChefFirstName));
               
            }
        }

        private string chefLastName;
        public string ChefLastName
        {
            get => chefLastName;
            set { chefLastName = value;
                IsModified = true;
                OnPropertyChanged(nameof(ChefLastName));
              
            }
        }

        public bool IsChefNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Має починатися з великої літери і містити тільки букви
            return Regex.IsMatch(name, @"^[А-ЯA-Z][а-яa-zА-ЯA-Z]*$");
        }

        //////////////////////////////////////////////


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        }


    }
}
