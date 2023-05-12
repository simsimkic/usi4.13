using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class RoomViewModel : ViewModelBase
    {
        private readonly Room _room;
        public string RoomName => _room.Name;
        public int RoomId => _room.Id;
        public string RoomType => Utility.Translate(_room.Type);


        public RoomViewModel(Room room)
        {
            _room = room;
        }
    }
}
