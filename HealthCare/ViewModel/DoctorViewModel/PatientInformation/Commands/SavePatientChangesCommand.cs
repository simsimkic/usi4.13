using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;
using System.Linq;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private readonly Hospital _hospital;
        private readonly Patient _selectedPatient;

        public SavePatientChangesCommand(Hospital hospital, Patient patient, PatientInforamtionViewModel viewModel)
        {
            _hospital = hospital;
            _viewModel = viewModel;
            _selectedPatient = patient;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();

                UpdatePatientMedicalRecord();
                ShowSuccessMessage();
            }
            catch (ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void UpdatePatientMedicalRecord()
        {
            float weight = _viewModel.Weight;
            float height = _viewModel.Height;
            string[] medicalHistory = _viewModel.PreviousDisease.ToArray();
            string[] allergies = _viewModel.Allergies.ToArray();
            MedicalRecord updatedMedicalRecord = new MedicalRecord(height, weight, medicalHistory, allergies);

            _hospital.PatientService.UpdatePatientMedicalRecord(_selectedPatient, updatedMedicalRecord);
        }

        private void ShowSuccessMessage()
        {
            Utility.ShowInformation("Pacijent uspesno sacuvan!");
        }

        private void Validate()
        {
            if (_viewModel.Weight <= 0)
            {
                throw new ValidationException("Neispravan unos tezine");
            }
            if (_viewModel.Height <= 0)
            {
                throw new ValidationException("Neispravan unos visine");
            }
        }
    }
}

