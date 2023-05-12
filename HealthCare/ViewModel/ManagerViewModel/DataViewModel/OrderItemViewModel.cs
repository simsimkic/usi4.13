using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class OrderItemViewModel : ViewModelBase
    {
        private readonly Equipment _equipment;
        private bool _isSelected;
        public bool IsSelected { 
            get => _isSelected;
            set {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        public string EquipmentName => _equipment.Name;
        public string EquipmentType => Utility.Translate(_equipment.Type);
        public int EquipmentId => _equipment.Id;
        public int CurrentQuantity { get; }

        private string _orderQuantity;
        public string OrderQuantity {
            get => _orderQuantity;
            set {
                _orderQuantity = value;
                IsSelected = Validation.IsNatural(value);
            }
        }

        public OrderItemViewModel(Equipment equipment, int currentQuantity)
        {
            _equipment = equipment;
            _isSelected = false;
            CurrentQuantity = currentQuantity;
            _orderQuantity = "0";
        }
    }
}
