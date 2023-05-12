using HealthCare.Context;
using HealthCare.Model;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.AppointmentView
{
    public partial class PatientRecordView : Window
    {
        PatientRecordViewModel model;
        public PatientRecordView(Hospital hospital)
        {
            model = new PatientRecordViewModel(hospital);
            DataContext = model;
            InitializeComponent();           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Sort(cbSort.SelectedValue.ToString());
        }

        private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.Filter(tbFilter.Text);
        }

        private void ListViewRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment appointment = (Appointment)listViewRecord.SelectedItem;

            if (listViewRecord.SelectedItems.Count == 1)
            {
                model.ShowAnamnesis(appointment);
            }
        }
    }
}
