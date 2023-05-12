using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView.RoomReservation;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class ShowReservationDialogCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _viewModel;

        public ShowReservationDialogCommand(Hospital hospital, DoctorMainViewModel viewModel)
        {
            _hospital = hospital;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                AppointmentViewModel selectedAppointment = _viewModel.SelectedAppointment;
                Appointment appointment = Schedule.GetAppointment(selectedAppointment.AppointmentID);
                new RoomReservationView(_hospital, appointment).Show();
            }
            catch (ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            AppointmentViewModel selectedAppointment = _viewModel.SelectedAppointment;
            if (selectedAppointment is null)
            {
                throw new ValidationException("Morate odabrati pregled iz tabele!");
            }

            Appointment appointment = Schedule.GetAppointment(selectedAppointment.AppointmentID);

            if (appointment.AnamnesisID == 0)
            {
                throw new ValidationException("Pacijent jos uvek nije primljen!");
            }

            if (!Schedule.HasAppointmentStarted(appointment))
            {
                throw new ValidationException("Pregled jos uvek nije poceo!");
            }
        }
    }
}
