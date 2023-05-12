using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public partial class PatientInformationView : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public PatientInformationView(Patient patient, Hospital hospital, bool isEdit)
        {
            InitializeComponent();

            DataContext = new PatientInforamtionViewModel(patient, hospital, isEdit);            
        }
    }
}
