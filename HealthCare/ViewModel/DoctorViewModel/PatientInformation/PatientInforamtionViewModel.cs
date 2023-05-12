using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation
{
    public class PatientInforamtionViewModel : ViewModelBase
    {
        private ObservableCollection<string> _previousDiseases;
        private ObservableCollection<string> _allergies;
        private Patient _selectedPatient;
        private Visibility _gridVisibility;
        private bool _isReadOnly = true;
        private bool _isFocusable = false;
        private string _name;
        private string _lastName;
        private string _jmbg;
        private DateTime _birthday;
        private string _gender;
        private float _height;
        private float _weight;
        private string _selectedDisease;
        private string _selectedAllergy;
        private string _disease;
        private string _allergy;

        public IEnumerable<string> PreviousDisease => _previousDiseases;
        public IEnumerable<string> Allergies => _allergies;
        public ICommand SaveChangesCommand { get; }
        public ICommand NewDiseaseCommand { get; }
        public ICommand RemoveDiseaseCommand { get; }
        public ICommand NewAllergyCommand { get; }
        public ICommand RemoveAllergyCommand { get; }
        public Visibility GridVisibility => _gridVisibility;
        public bool IsFocusable => _isFocusable;
        public bool IsReadOnly => _isReadOnly;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string JMBG
        {
            get => _jmbg;
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(JMBG));
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public string SelectedDisease
        {
            get => _selectedDisease;
            set
            {
                _selectedDisease = value;
                OnPropertyChanged(nameof(SelectedDisease));
            }
        }
        public string SelectedAllergy
        {
            get => _selectedAllergy;
            set
            {
                _selectedAllergy = value;
                OnPropertyChanged(nameof(SelectedAllergy));
            }
        }
        public string Disease
        {
            get => _disease;
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Disease));
            }
        }
        public string Allergy
        {
            get => _allergy;
            set
            {
                _allergy = value;
                OnPropertyChanged(nameof(Allergy));
            }
        }
        public PatientInforamtionViewModel(Patient patient, Hospital hospital, bool isEditing)
        {
            _selectedPatient = patient;
            _isFocusable = isEditing;
            _isReadOnly = !isEditing;

            SaveChangesCommand = new SavePatientChangesCommand(hospital, patient, this);
            NewDiseaseCommand = new AddDiseaseCommand(this);
            RemoveDiseaseCommand = new RemoveDiseaseCommand(this);
            NewAllergyCommand = new AddAllergyCommand(this);
            RemoveAllergyCommand = new RemoveAllergyCommand(this);

            _allergies = new ObservableCollection<string>();
            _previousDiseases = new ObservableCollection<string>();
            LoadDataIntoView(patient, isEditing);
        }
        public void LoadDataIntoView(Patient patient, bool isEditing)
        {
            Name = patient.Name;
            LastName = patient.LastName;
            Birthday = patient.BirthDate;
            Gender = Utility.Translate(patient.Gender);
            JMBG = patient.JMBG;

            if (patient.MedicalRecord != null)
            {
                Weight = patient.MedicalRecord.Weight;
                Height = patient.MedicalRecord.Height;
                if (patient.MedicalRecord.MedicalHistory != null)
                {
                    Update();
                }
            }
            if (isEditing)
            {
                _gridVisibility = Visibility.Visible;
            }
            else
            {
                _gridVisibility = Visibility.Collapsed;
            }

        }
        private void Update()
        {
            UpdateAllergies();
            UpdateDisease();
        }
        
        private void UpdateDisease()
        {
            _previousDiseases.Clear();
            foreach (var disease in _selectedPatient.MedicalRecord.MedicalHistory)
            {
                _previousDiseases.Add(disease);
            }
        }
        private void UpdateAllergies()
        {
            _allergies.Clear();
            foreach (var allergy in _selectedPatient.MedicalRecord.Allergies)
            {
                _allergies.Add(allergy);
            }
        }
        public void AddPreviousDisease(string disease)
        {
            _previousDiseases.Add(disease);
        }
        public void RemovePreviousDisease(string disease)
        {
            _previousDiseases.Remove(disease);
        }
        public void AddAllergy(string allergy)
        {
            _allergies.Add(allergy);
        }
        public void RemoveAllergy(string allergy)
        {
            _allergies.Remove(allergy);
        }

    }
}
