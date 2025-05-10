using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DishDTO> Dishes { get; set; } = new ObservableCollection<DishDTO>();
        public OrderViewModel() 
        {
           
        }

        public OrderViewModel(OrderDTO dto) 
        {
            CafeName = dto.CafeName;

            foreach (var dishDto in dto.Dishes)
            {
                Dishes.Add(dishDto);
            }
            IsDirty = false;
        }

        private bool isDirty = false;
        public bool IsDirty
        {
            get => isDirty;
            set
            {
                isDirty = value;
                OnPropertyChanged(nameof(IsDirty));
            }
        }

        private string cafeName;
        public string CafeName
        {
            get => cafeName;
            set
            {
                cafeName = value;
                IsDirty = true;
                OnPropertyChanged(nameof(CafeName));
            }
        }

        private DishDTO selectedDish;
        public DishDTO SelectedDish
        {
            get => selectedDish;
            set
            {
                selectedDish = value;

                OnPropertyChanged(nameof(SelectedDish));
                OnPropertyChanged(nameof(CanEditDish)); // для кнопки "Редагувати"
            }
        }

        // Це для активації кнопки
        public bool CanEditDish => SelectedDish != null;

        public void DeleteSelectedDish()
        {
            if (SelectedDish != null && Dishes.Contains(SelectedDish))
            {
                Dishes.Remove(SelectedDish);
                SelectedDish = null;
                IsDirty = true;
                OnPropertyChanged(nameof(SelectedDish));
                OnPropertyChanged(nameof(CanEditDish));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
