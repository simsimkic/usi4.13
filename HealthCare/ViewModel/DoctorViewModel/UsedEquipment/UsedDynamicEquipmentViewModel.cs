using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment
{
    public class UsedDynamicEquipmentViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private readonly int _roomId;
        private ObservableCollection<EquipmentViewModel> _usedDynamicEquipment;
        public IEnumerable<EquipmentViewModel> UsedDynamicEquipment => _usedDynamicEquipment;

        private int _itemQuantity;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                OnPropertyChanged(nameof(ItemQuantity));
            }
        }

        public ICommand ResetEquipmentCommand { get; }
        public ICommand EndExaminationCommand { get; }
        public UsedDynamicEquipmentViewModel(Hospital hospital, Window window, int roomId) 
        { 
            _hospital = hospital;
            _roomId = roomId;
            _usedDynamicEquipment = new ObservableCollection<EquipmentViewModel>();

            ResetEquipmentCommand = new ResetEquipmentQuantityCommand(this);
            EndExaminationCommand = new EndEquipmentQuantityEditingCommand(hospital, window, this);

            Update();
        }

        public void Update()
        {
            _usedDynamicEquipment.Clear();

            foreach(InventoryItem inventoryItem in _hospital.Inventory.GetRoomItems(_roomId))
            {
                Equipment equipment = _hospital.EquipmentService.Get(inventoryItem.EquipmentId);
                if (equipment.IsDynamic)
                {
                    _usedDynamicEquipment.Add(new EquipmentViewModel(equipment, inventoryItem));
                }
            }

        }
    }
}
