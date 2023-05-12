using HealthCare.Model;
using HealthCare.View;
using System.Windows.Media;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryItemViewModel
    {
        private readonly InventoryItem _item;
        public readonly Equipment Equipment;
        public readonly Room Room;
        public string EquipmentName => Equipment.Name;
        public string EquipmentType => Utility.Translate(Equipment.Type);
        public string RoomName => Room.Name;
        public string RoomType => Utility.Translate(Room.Type);
        public int Quantity => _item.Quantity;
        public string IsDynamic => Utility.Translate(Equipment.IsDynamic);
        public Brush Color => Quantity < 5 ? Brushes.Red : Brushes.Black;

        public InventoryItemViewModel(InventoryItem item, Equipment equipment, Room room)
        {
            _item = item;
            Equipment = equipment;
            Room = room;
        }
    }
}
