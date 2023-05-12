using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    class DeleteAppointmentCommand : CommandBase
    {
        private readonly DoctorMainViewModel _viewModel;
        public DeleteAppointmentCommand(DoctorMainViewModel mainViewModel)
        {
            _viewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                AppointmentViewModel a = _viewModel.SelectedAppointment;
                Appointment appointment = Schedule.GetAppointment(a.AppointmentID);

                Schedule.DeleteAppointment(appointment.AppointmentID);
                _viewModel.Update();
            }
            catch (ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {

            var selectedAppointmentId = _viewModel.SelectedAppointment?.AppointmentID;
            if (selectedAppointmentId is null)
            {
                throw new ValidationException("Odaberite pregled/operaciju iz tabele!");
            }

            Appointment selectedAppointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointmentId));
            if (selectedAppointment is null)
            {
                throw new ValidationException("Ups Doslo je do greske!");
            }
        }
    }
}
