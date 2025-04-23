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


        private string cafeName;
        public string CafeName
        {
            get => cafeName;
            set
            {
                cafeName = value;
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
