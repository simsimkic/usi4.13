using HealthCare.Model;
using HealthCare.Service;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PatientViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        private PatientService _patientService;

        public PatientViewModel(PatientService patientService)
        {
            Patients = new ObservableCollection<Patient>();
            _patientService = patientService;
        }

        public void Update()
        {
            Patients.Clear();
            foreach (var patient in _patientService.GetAll())
                Patients.Add(patient);
        }
    }
}
