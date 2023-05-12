using HealthCare.Context;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class EquipmentOrderViewModel : ViewModelBase
    {
        private readonly Inventory _inventory;
        private readonly EquipmentService _equipmentService;
        public ObservableCollection<OrderItemViewModel> Items { get; }

        public EquipmentOrderViewModel(Hospital hospital)
        {
            _inventory = hospital.Inventory;
            _equipmentService = hospital.EquipmentService;

            Items = new ObservableCollection<OrderItemViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            var items = new List<OrderItemViewModel>();
            foreach (int id in _inventory.GetLowQuantityEquipment())
            {
                var equipment = _equipmentService.Get(id);
                if (!equipment.IsDynamic)
                    continue;

                var quantity = _inventory.GetTotalQuantity(id);
                items.Add(new OrderItemViewModel(equipment, quantity));
            }

            Sort(items).ForEach(x => Items.Add(x));
        }

        public List<OrderItemViewModel> Sort(List<OrderItemViewModel> items)
        {
            return items.OrderBy(x => x.CurrentQuantity).ToList();
        }
    }
}
