using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class FinishExaminationCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly Window _window;
        private readonly DoctorExamViewModel _viewModel;
        private readonly Appointment _appointment;
        private readonly int _roomId;

        public FinishExaminationCommand(Hospital hospital, Window window, Appointment appointment, DoctorExamViewModel viewModel, int roomId) 
        {
            _hospital = hospital;
            _viewModel = viewModel;
            _window = window;
            _appointment = appointment;
            _roomId = roomId;
        }
        public override void Execute(object parameter)
        {
            _window.Close();

            string conclusion = _viewModel.Conclusion;
            
            Anamnesis anamnesis = _hospital.AnamnesisService.Get(_appointment.AnamnesisID);
            anamnesis.DoctorsObservations = conclusion;
            _hospital.AnamnesisService.Update(anamnesis);

            new UsedDynamicEquipmentView(_hospital, _roomId).Show();
        }
    }
}
