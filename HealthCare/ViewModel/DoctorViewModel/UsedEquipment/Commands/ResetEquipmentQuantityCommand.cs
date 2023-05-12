using HealthCare.Command;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands
{
    public class ResetEquipmentQuantityCommand : CommandBase
    {
        private readonly UsedDynamicEquipmentViewModel _viewModel;

        public ResetEquipmentQuantityCommand(UsedDynamicEquipmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.Update();
        }
    }
}
