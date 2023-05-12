using HealthCare.Context;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    public partial class PatientSearchView : Window
    {
        public PatientSearchView(Hospital hospital)
        {
            InitializeComponent();
            DataContext = new PatientSearchViewModel(hospital);

        }
    }
}
