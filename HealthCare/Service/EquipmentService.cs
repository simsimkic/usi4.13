using HealthCare.Model;

namespace HealthCare.Service
{
    public class EquipmentService : NumericService<Equipment>
    {
        public EquipmentService(string filepath) : base(filepath) { }
    }
}
