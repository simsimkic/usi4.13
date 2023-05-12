using HealthCare.Context;
using HealthCare.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RearrangingViewModel
    {
        private readonly Hospital _hospital;
        public ObservableCollection<InventoryItemViewModel> FromRooms { get; set; }
        public ObservableCollection<InventoryItemViewModel> ToRooms { get; set;}
        public List<Equipment> Equipment => _hospital.EquipmentService.GetAll();

        public RearrangingViewModel(Hospital hospital)
        {
            _hospital = hospital;

            FromRooms = new ObservableCollection<InventoryItemViewModel>();
            ToRooms = new ObservableCollection<InventoryItemViewModel>();
        }

        public void Load(Equipment equipment)
        {
            FromRooms.Clear();
            ToRooms.Clear();
            var fromRooms = new List<InventoryItemViewModel>();
            var toRooms = new List<InventoryItemViewModel>();

            foreach (Room room in _hospital.RoomService.GetAll()) {
                var item = new InventoryItem(0, equipment.Id, room.Id, 0);
                var found = _hospital.Inventory.GetAll().Find(x => x.Equals(item));

                if (found is not null) {
                    item.Quantity = found.Quantity;

                    if (item.Quantity > 0)
                        fromRooms.Add(new InventoryItemViewModel(item, equipment, room));
                }
                else
                    item.Quantity = 0;

                toRooms.Add(new InventoryItemViewModel(item, equipment, room));
            }

            Sort(fromRooms, -1).ForEach(model => FromRooms.Add(model));
            Sort(toRooms).ForEach(model => ToRooms.Add(model));
        }

        public List<InventoryItemViewModel> Sort(List<InventoryItemViewModel> items, int order=1)
        {
            IEnumerable<InventoryItemViewModel> sorted;
            if (order == -1)
                sorted = items.OrderByDescending(x => x.Quantity).ThenBy(x => x.RoomName);
            else
                sorted = items.OrderBy(x => x.Quantity).ThenBy(x => x.RoomName);
            return sorted.ToList();
        }
    }
}
