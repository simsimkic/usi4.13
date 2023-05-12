using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class DoctorMainViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private ObservableCollection<AppointmentViewModel> _appointments;
        private DateTime _startDate = DateTime.Now;
        private int _numberOfDays = 3;
        private AppointmentViewModel _selectedAppointment;

        public IEnumerable<AppointmentViewModel> Appointments => _appointments;
        public ICommand CreateAppointmentViewCommand { get; }
        public ICommand EditAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }
        public ICommand ShowDetailedPatientInfoCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand ShowPatientSearchCommand { get; }
        public ICommand StartExaminationCommand { get; }
        public ICommand ResetFilterCommand { get; }
        public ICommand LogOutCommand { get; }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public int NumberOfDays
        {
            get { return _numberOfDays; }
            set
            {
                _numberOfDays = value;
                OnPropertyChanged(nameof(NumberOfDays));
            }
        }

        public AppointmentViewModel SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }

        public DoctorMainViewModel(Hospital hospital, Window window)
        {
            _hospital = hospital;
            _appointments = new ObservableCollection<AppointmentViewModel>();
            Update();

            ResetFilterCommand = new ResetFilterCommand(this);
            LogOutCommand = new LogOutCommand(window);
            CreateAppointmentViewCommand = new MakeAppointmentNavigationCommand(hospital, this);
            EditAppointmentCommand = new EditAppointmentDoctorCommand(hospital, this);
            DeleteAppointmentCommand = new DeleteAppointmentCommand(this);
            ShowDetailedPatientInfoCommand = new ShowPatientInfoCommand(hospital, this, false);
            ApplyFilterCommand = new ApplyFilterCommand(hospital, this);
            ShowPatientSearchCommand = new ShowPatientSearchViewCommand(hospital);
            StartExaminationCommand = new ShowReservationDialogCommand(hospital, this);
        }

        public void ApplyFilterOnAppointments(List<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (var appointment in appointments)
            {
                _appointments.Add(new AppointmentViewModel(appointment));
            }
        }

        public void Update()
        {
            _appointments.Clear();
            foreach (var appointment in Schedule.GetDoctorAppointments((Doctor)_hospital.Current))
            {
                  _appointments.Add(new AppointmentViewModel(appointment));
            }
        }
    }
}
