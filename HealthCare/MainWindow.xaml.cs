using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.AppointmentView;
using HealthCare.View.DoctorView;
using HealthCare.View.ManagerView;
using HealthCare.View.PatientView;
using System.ComponentModel;
using System.Windows;

namespace HealthCare
{
    public partial class MainWindow : Window
    {
        private readonly Hospital _hospital;

        public MainWindow()
        {
            InitializeComponent();

            _hospital = new Hospital();
            _hospital.LoadAll();
        }

        private void btnQuitApp_Click(object sender, RoutedEventArgs e)
        {
            _hospital.SaveAll();
            ExitApp();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            try
            {
                switch (_hospital.LoginRole(username, password))
                {
                    case UserRole.Manager:
                        new ManagerMenu(this, _hospital).Show();
                        break;
                    case UserRole.Doctor:
                        ShowNotifications();
                        new DoctorMainView(this, _hospital).Show();
                        break;
                    case UserRole.Nurse:
                        new NurseMenu(this, _hospital).Show();
                        break;
                    case UserRole.Patient:
                        ShowNotifications();
                        new AppointmentMainView(_hospital).Show();
                        break;
                }

                txtUserName.Clear();
                txtPassword.Clear();
                Hide();
            }
            catch (LoginException ex)
            {
                Utility.ShowWarning(ex.Message);
            }
        }

        private void ShowNotifications()
        {
            if (_hospital.Current is null)
                return;

            foreach (Notification notification in _hospital.NotificationService.GetForUser(_hospital.Current.JMBG))
            {
                Utility.ShowInformation(notification.Display());
                _hospital.NotificationService.Update(notification);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ExitApp();
        }

        public void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
