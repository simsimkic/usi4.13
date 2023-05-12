using HealthCare.Context;
using HealthCare.View.ReceptionView;
using HealthCare.View.UrgentAppointmentView;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.PatientView
{
    public partial class NurseMenu : Window
    {
        private readonly Hospital _hospital;
        private MainWindow _window;
        public NurseMenu(MainWindow window,Hospital hospital)
        {
            InitializeComponent();
            _window = window;
            _hospital = hospital;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _window.Show();
        }

        private void mnuCRUD_Click(object sender, RoutedEventArgs e)
        {
            new NurseMainView(_hospital).ShowDialog();
        }

        private void mnuReception_Click(object sender, RoutedEventArgs e)
        {
            new MainReceptionView(_hospital).ShowDialog();
        }

        private void mnuUrgent_Click(object sender, RoutedEventArgs e)
        {
            new UrgentView(_hospital).ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _window.Show();
        }
    }
}
