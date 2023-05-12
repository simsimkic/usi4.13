using HealthCare.Command;
using HealthCare.Context;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;


namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class MakeAppointmentNavigationCommand : CommandBase
    {

        private DoctorMainViewModel _viewModel;
        private readonly Hospital _hospital;
        public MakeAppointmentNavigationCommand(Hospital hospital, DoctorMainViewModel viewModel)
        {
            _viewModel = viewModel;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            new MakeAppointmentView(_hospital, _viewModel).ShowDialog();
        }
    }
}
