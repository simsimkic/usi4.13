using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class RemoveAllergyCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        public RemoveAllergyCommand(PatientInforamtionViewModel viewModel) 
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                string selectedAllergy = _viewModel.SelectedAllergy;
                _viewModel.RemoveAllergy(selectedAllergy);
            
            } catch(ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }

        }
        private void Validate()
        {
            if(_viewModel.SelectedAllergy is null)
            {
                throw new ValidationException("Morate odabrati alergiju koju zelite da uklonite.");
            }
        }
    }
}
