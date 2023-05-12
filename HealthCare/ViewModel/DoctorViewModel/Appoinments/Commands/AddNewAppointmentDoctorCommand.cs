using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.Appoinments.Commands
{
    public class AddNewAppointmentDoctorCommand : CommandBase
    {
        private readonly MakeAppointmentViewModel _makeAppointmentViewModel;
        private readonly DoctorMainViewModel _doctorMainViewModel;
        private readonly Window _window;
        private readonly Hospital _hospital;
        private readonly bool _isEditing;

        public AddNewAppointmentDoctorCommand(Hospital hospital, MakeAppointmentViewModel viewModel, DoctorMainViewModel docMainViewModel, Window window, bool isEditing)
        {
            _makeAppointmentViewModel = viewModel;
            _doctorMainViewModel = docMainViewModel;
            _window = window;
            _hospital = hospital;
            _isEditing = isEditing;
        }

        public override void Execute(object parameter)
        {
            if (_makeAppointmentViewModel.SelectedPatient is null)
            {
                Utility.ShowWarning("Morate odabrati pacijenta!");
                return;
            }

            Patient? patient = _hospital.PatientService.GetAccount(_makeAppointmentViewModel.SelectedPatient.JMBG);
            if (patient is null)
            {
                Utility.ShowError("Oops... Doslo je do greske probajte ponovo!");
                return;
            }

            Appointment newAppointment = MakeAppointment(patient);

            if (_isEditing)
            {
                EditAppointment(newAppointment);
            }
            else
            {
                if (!Schedule.CreateAppointment(newAppointment))
                {
                    Utility.ShowWarning("Doktor ili pacijent je zauzet u ovom terminu, odaberite drugi termin");
                    return;
                }
                else
                {
                    _doctorMainViewModel.Update();
                    _window.Close();
                }
            }

        }

        private void EditAppointment(Appointment newAppointment)
        {
            newAppointment.AppointmentID = Convert.ToInt32(_doctorMainViewModel.SelectedAppointment.AppointmentID);
            if (!Schedule.UpdateAppointment(newAppointment))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u ovom terminu, odaberite drugi termin");
                return;
            }

            _doctorMainViewModel.Update();
            _window.Close();
        }

        private Appointment MakeAppointment(Patient patient)
        {
            DateTime start = _makeAppointmentViewModel.StartDate.Date + new TimeSpan(_makeAppointmentViewModel.Hours, _makeAppointmentViewModel.Minutes, 0);
            TimeSpan duration = TimeSpan.FromMinutes(_makeAppointmentViewModel.IsOperation ? _makeAppointmentViewModel.Duration : 15);
            TimeSlot timeSlot = new TimeSlot(start, duration);

            Appointment newAppointment = new Appointment(patient, (Doctor)_hospital.Current, timeSlot, _makeAppointmentViewModel.IsOperation);
            return newAppointment;
        }
    }
}
