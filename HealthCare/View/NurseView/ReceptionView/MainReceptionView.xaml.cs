using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.PatientView;
using System.Windows;

namespace HealthCare.View.ReceptionView
{
    public partial class MainReceptionView : Window 
    {
        private readonly Hospital _hospital;
        public MainReceptionView(Hospital hospital)
        {
            InitializeComponent();
            _hospital = hospital;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string JMBG = tbJMBG.Text.Trim();
            Patient? patient = _hospital.PatientService.GetAccount(JMBG);

            if(patient is null)
            {
                new CreatePatientView(_hospital,JMBG).ShowDialog();
                return;
            }

            Appointment? starting = Schedule.TryGetReceptionAppointment(patient);
            if (starting is null)
            {
                Utility.ShowWarning("Pacijent nema preglede u narednih 15 minuta.");
                return;
            }
            new NurseAnamnesisView(_hospital, starting.AppointmentID, patient).ShowDialog();
        }
    }
}
