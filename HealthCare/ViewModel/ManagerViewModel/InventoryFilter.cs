using HealthCare.Model;
using HealthCare.View;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    internal class InventoryFilter
    {
        private List<InventoryItemViewModel> _items;

        public InventoryFilter(List<InventoryItemViewModel> items)
        {
            _items = items;
        }

        public List<InventoryItemViewModel> GetFiltered()
        {
            return _items;
        }

        public void FilterQuantity(params bool[] quantities)
        {
            if (!(quantities[0] || quantities[1] || quantities[2])) return;

            _items = _items.Where(x =>
                quantities[0] && x.Quantity == 0 ||
                quantities[1] && x.Quantity <= 10 ||
                quantities[2] && x.Quantity > 10).ToList();
        }

        public void FilterEquipmentType(params bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3])) return;

            _items = _items.Where(x =>
                types[0] && x.Equipment.Type.Equals(EquipmentType.Examinational) ||
                types[1] && x.Equipment.Type.Equals(EquipmentType.Operational) ||
                types[2] && x.Equipment.Type.Equals(EquipmentType.RoomFurniture) ||
                types[3] && x.Equipment.Type.Equals(EquipmentType.HallwayFurniture)).ToList();
        }

        public void FilterRoomType(params bool[] types)
        {
            if (!(types[0] || types[1] || types[2] || types[3] || types[4])) return;

            _items = _items.Where(x => 
                types[0] && x.Room.Type.Equals(RoomType.Examinational) ||
                types[1] && x.Room.Type.Equals(RoomType.Operational) ||
                types[2] && x.Room.Type.Equals(RoomType.PatientCare) ||
                types[3] && x.Room.Type.Equals(RoomType.Reception) ||
                types[4] && x.Room.Type.Equals(RoomType.Warehouse)).ToList();
        }

        public void FilterAnyProperty(string query)
        {
            if (query == "") return;
            string[] tokens = query.Split(' ');

            _items = _items.Where(x => 
                HasAllTokens(x, tokens)).ToList();
        }

        private bool HasAllTokens(InventoryItemViewModel item, string[] tokens)
        {
            tokens = NormalizeTokens(tokens);
            return tokens.Count(x =>
                    ContainsToken(item.Equipment.Name, x) ||
                    ContainsToken(item.Room.Name, x) ||
                    ContainsToken(item.Quantity.ToString(), x) ||
                    ContainsToken(Utility.Translate(item.Equipment.Type), x) ||
                    ContainsToken(Utility.Translate(item.Room.Type), x) ||
                    ContainsToken(Utility.Translate(item.Equipment.IsDynamic), x))
                == tokens.Length;
        }

        private string[] NormalizeTokens(string[] tokens)
        {
            return tokens.Select(x => x.Trim().ToLower()).Where(x => x != "").ToArray();
        }

        private bool ContainsToken(string text, string token)
        {
            return text.ToLower().Contains(token);
        }
    }
}
