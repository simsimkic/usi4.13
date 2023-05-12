using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation
{
    public class PatientSearchViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;

        private ObservableCollection<PatientViewModel> _patients;
        public IEnumerable<PatientViewModel> Patients => _patients;

        private PatientViewModel _selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public ICommand ShowEditPatientCommand { get; }

        public PatientSearchViewModel(Hospital hospital)
        {
            _hospital = hospital;
            _patients = new ObservableCollection<PatientViewModel>();
            ShowEditPatientCommand = new ShowPatientInfoCommand(hospital, this, true);
            Update();
        }

        public void Update()
        {
            _patients.Clear();
            foreach (var patient in _hospital.DoctorService.GetExaminedPatients((Doctor)_hospital.Current))
            {
                _patients.Add(new PatientViewModel(patient));

            }
        }

    }
}
