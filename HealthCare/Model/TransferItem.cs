using HealthCare.Repository;
using System;
using System.Linq;

namespace HealthCare.Model
{
    public class TransferItem : OrderItem
    {
        public override object Key { get => Id; set => Id = (int) value; }
        public int FromRoom { get; set; }
        public int ToRoom { get; set; }

        public TransferItem() : this(0, 0, DateTime.MinValue, false, 0, 0) { }
        public TransferItem(int equipmentId, int quantity, DateTime scheduled, bool executed, int fromRoom, int toRoom) :
            this(0, equipmentId, quantity, scheduled, executed, fromRoom, toRoom) { }
        public TransferItem(int id, int equipmentId, int quantity, DateTime scheduled, bool executed, int fromRoom, int toRoom)
            : base(id, equipmentId, quantity, scheduled, executed)
        {
            FromRoom = fromRoom;
            ToRoom = toRoom;
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(Utility.SubArray(values, 0, 5));
            FromRoom = int.Parse(values[5]);
            ToRoom = int.Parse(values[6]);
        }

        public override string[] ToCSV()
        {
            return base.ToCSV().Concat(
                new string[] { FromRoom.ToString(), ToRoom.ToString() }).ToArray();
        }
    }
}
