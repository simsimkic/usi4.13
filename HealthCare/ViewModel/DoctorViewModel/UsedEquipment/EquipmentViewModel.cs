using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment
{

    public class EquipmentViewModel : ViewModelBase
    {
        private Equipment _equipment;
        private InventoryItem _inventoryItem;
        private int _currentQuantity;
        
        public ICommand UseEquipment { get; }
        
        public string EquipmentName => _equipment.Name;
        public int EquipmentId => _equipment.Id;
        public int InventoryId => _inventoryItem.Id;
        public int CurrentQuantity {
            get { return _currentQuantity; }
            set
            {
                _currentQuantity = value;
                OnPropertyChanged(nameof(CurrentQuantity));
            }
        }

        public EquipmentViewModel(Equipment equipment, InventoryItem inventoryItem)
        {
            _equipment = equipment;
            _inventoryItem = inventoryItem;
            _currentQuantity = _inventoryItem.Quantity;
            UseEquipment = new QuantityChangeCommand(this);
        }

       
    }
}
