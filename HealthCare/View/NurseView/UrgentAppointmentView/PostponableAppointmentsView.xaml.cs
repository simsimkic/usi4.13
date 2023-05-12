using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.UrgentAppointmentView
{
    public partial class PostponableAppointmentsView : Window
    {
        private PostponableAppointmentsViewModel _model;
        private AppointmentViewModel? _selected;
        private Appointment _newAppointment;
        private readonly Hospital _hospital;
        public PostponableAppointmentsView(Appointment newAppointment, List<Appointment> postponable, Hospital hospital)
        {
            InitializeComponent();
            _newAppointment = newAppointment;
            _hospital = hospital;
            _model = new PostponableAppointmentsViewModel(postponable);
            DataContext = _model;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = (AppointmentViewModel)lvAppointments.SelectedItem;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_selected is null)
            {
                Utility.ShowWarning("Izaberite polje iz tabele");
                return;
            }
            _newAppointment.TimeSlot.Start = _selected.Appointment.TimeSlot.Start;
            _newAppointment.Doctor = _selected.Appointment.Doctor;
            Schedule.PostponeAppointment(_selected.Appointment);
            Schedule.CreateUrgentAppointment(_newAppointment);

            _hospital.NotificationService.Add(new Notification(
                "Termin sa ID-jem " + _selected.Appointment.AppointmentID + " je pomeren.",
                _selected.Appointment.Doctor.JMBG, _selected.Appointment.Patient.JMBG));
            _hospital.NotificationService.Add(new Notification(
                "Hitan termin sa ID-jem " + _selected.Appointment.AppointmentID + " je kreiran.",
                _selected.Appointment.Doctor.JMBG));

            Utility.ShowInformation("Uspesno odlozen termin.");
            Close();
        }

    }
}