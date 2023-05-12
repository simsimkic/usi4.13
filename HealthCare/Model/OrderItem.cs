using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class OrderItem : Identifier, ISerializable
    {
        public override object Key { get => Id; set => Id = (int)value; }
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime Scheduled { get; set; }
        public bool Executed { get; set; }

        public OrderItem() : this(0, 0, DateTime.MinValue, false) { }
        public OrderItem(int equipmentId, int quantity, DateTime scheduled, bool executed) : 
            this(0, equipmentId, quantity, scheduled, executed) { }
        public OrderItem(int id, int equipmentId, int quantity, DateTime scheduled, bool executed)
        {
            Id = id;
            EquipmentId = equipmentId;
            Quantity = quantity;
            Scheduled = scheduled;
            Executed = executed;
        }

        public virtual void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentId = int.Parse(values[1]);
            Quantity = int.Parse(values[2]);
            Scheduled = Utility.ParseDate(values[3]);
            Executed = bool.Parse(values[4]);
        }

        public virtual string[] ToCSV()
        {
            return new string[] { 
                Id.ToString(), EquipmentId.ToString(), 
                Quantity.ToString(), Utility.ToString(Scheduled), 
                Executed.ToString() };
        }
    }
}
