using HealthCare.Repository;

namespace HealthCare.Model
{
    public enum RoomType
    {
        Examinational,
        Operational,
        PatientCare,
        Reception,
        Warehouse
    }
    public class Room : Identifier, ISerializable
    {
        public override object Key { get => Id; set => Id = (int)value; }
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }

        public Room() : this(0, "", RoomType.Warehouse) { }
        public Room(int id, string name, RoomType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Name, Type.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Type = Utility.Parse<RoomType>(values[2]);
        }
    }
}
