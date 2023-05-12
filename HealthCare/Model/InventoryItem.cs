using HealthCare.Repository;

namespace HealthCare.Model
{
    public class InventoryItem : Identifier, ISerializable
    {
        public override object Key {
            get => Id;
            set { Id = (int)value; } 
        }
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }

        public InventoryItem() : this(0) { }
        public InventoryItem(int id) : this(id, 0, 0, 0) { }
        public InventoryItem(int equipmentId, int roomId, int quantity) :
            this(0, equipmentId, roomId, quantity) { }
        public InventoryItem(int id, int equipmentId, int roomId, int quantity)
        {
            Id = id;
            EquipmentId = equipmentId;
            RoomId = roomId;
            Quantity = quantity;
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                EquipmentId.ToString(),
                RoomId.ToString(),
                Quantity.ToString()};
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentId = int.Parse(values[1]);
            RoomId = int.Parse(values[2]);
            Quantity = int.Parse(values[3]);
        }

        public override bool Equals(object? obj)
        {
            return obj is InventoryItem item && (Id == item.Id || 
                (EquipmentId == item.EquipmentId && RoomId == item.RoomId));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
