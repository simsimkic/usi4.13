using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class AddAllergyCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;

        public AddAllergyCommand(PatientInforamtionViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _viewModel.AddAllergy(_viewModel.Allergy);
                
            } catch(ValidationException ve) 
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Allergy))
            {
                throw new ValidationException("Morate uneti alergiju u polje");
            }
        }
    }
}
