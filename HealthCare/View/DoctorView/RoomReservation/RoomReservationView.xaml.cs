using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.RoomReservation;
using System.Windows;

namespace HealthCare.View.DoctorView.RoomReservation
{
    public partial class RoomReservationView : Window
    {
        public RoomReservationView(Hospital hospital, Appointment appointment)
        {
            InitializeComponent();
            DataContext = new RoomReservationViewModel(hospital, appointment, this);
        }
    }
}
