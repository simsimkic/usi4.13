using HealthCare.Model;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class AppointmentViewModel : ViewModelBase
    {
        private readonly Appointment _appointment;
        public int AppointmentID => _appointment.AppointmentID;
        public string Patient => _appointment.Patient.Name + " " + _appointment.Patient.LastName;
        public string Doctor => _appointment.Doctor.Name + " " + _appointment.Doctor.LastName;
        public string StartingTime => _appointment.TimeSlot.Start.ToString();
        public string Duration => _appointment.TimeSlot.Duration.ToString();
        public bool IsOperation => _appointment.IsOperation;
        public string JMBG => _appointment.Patient.JMBG;

        public AppointmentViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }

    }
}
