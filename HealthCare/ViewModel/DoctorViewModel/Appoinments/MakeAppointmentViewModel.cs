using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Appoinments.Commands;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Appoinments
{
    public class MakeAppointmentViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private readonly Patient _selected;
        private ObservableCollection<PatientViewModel> _patients;

        private DateTime _startDate = DateTime.Today;
        private int _hours = 0;
        private int _minutes = 0;
        private bool _isOperation;
        private int _duration = 15;
        private PatientViewModel _selectedPatient;

        public IEnumerable<PatientViewModel> Patients => _patients;

        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value < DateTime.Today)
                {
                    _startDate = DateTime.Today;
                }
                else
                {
                    _startDate = value;
                }
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public int Hours
        {
            get { return _hours; }
            set
            {
                if (value > 23 || value < 0)
                {
                    _hours = 0;
                }
                else
                {
                    _hours = value;
                }
                OnPropertyChanged(nameof(Hours));
            }
        }
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (value > 59 || value < 0)
                {
                    _minutes = 0;
                }
                else
                {
                    _minutes = value;
                }
                OnPropertyChanged(nameof(Minutes));
            }
        }
        public bool IsOperation
        {
            get { return _isOperation; }
            set
            {
                _isOperation = value;
                OnPropertyChanged(nameof(IsOperation));
            }
        }
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 15)
                {
                    _duration = 15;
                }
                else
                {
                    _duration = value;
                }
                OnPropertyChanged(nameof(Duration));
            }
        }
        public PatientViewModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
        public MakeAppointmentViewModel(Hospital hospital, DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For New Appointment
            _hospital = hospital;
            CancelCommand = new CancelCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(hospital, this, DoctorViewModel, window, false);
            _patients = new ObservableCollection<PatientViewModel>();
            Update();
        }

        public MakeAppointmentViewModel(Hospital hospital, Appointment appointment, DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For Editing Appointment
            _hospital = hospital;
            _startDate = appointment.TimeSlot.Start;
            _hours = Convert.ToInt32(appointment.TimeSlot.Start.TimeOfDay.TotalHours);
            _minutes = appointment.TimeSlot.Start.Minute;
            _isOperation = appointment.IsOperation;
            _duration = Convert.ToInt32(appointment.TimeSlot.Duration.TotalMinutes);
            _patients = new ObservableCollection<PatientViewModel>();
            _selected = appointment.Patient;
            Update();

            CancelCommand = new CancelCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(_hospital, this, DoctorViewModel, window, true);

        }
        public void Update()
        {
            _patients.Clear();
            foreach (Patient patient in _hospital.PatientService.GetAll())
            {
                if (_selected == patient) { SelectedPatient = new PatientViewModel(patient); }
                _patients.Add(new PatientViewModel(patient));
            }
        }

    }
}
