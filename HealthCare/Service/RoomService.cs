using HealthCare.Model;
using System.Collections.Generic;

namespace HealthCare.Service
{
    public class RoomService : NumericService<Room>
    {
        public RoomService(string filepath) : base(filepath) { }

        public int GetWarehouseId()
        {
            var warehouses = GetRoomsByType(RoomType.Warehouse);
            if (warehouses.Count == 0) 
                throw new KeyNotFoundException();

            return warehouses[0].Id;
        }

        public List<Room> GetRoomsByType(RoomType roomType)
        {
            return GetAll().FindAll(x => x.Type == roomType);
        }
    }
}
