using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.PatientView
{
    public partial class NurseMainView : Window
    {
        private Hospital _hospital;
        private PatientViewModel _model;
        private Patient? _patient;
        public MedicalRecord? _record;
        private List<TextBox> _textBoxes;

        public NurseMainView(Hospital hospital)
        {
            InitializeComponent();

            _hospital = hospital;

            _model = new PatientViewModel(_hospital.PatientService);
            DataContext = _model;

            _model.Update();
            _patient = null;
            _record = null;

            _textBoxes = new List<TextBox> { tbName, tbLastName, tbAddress, tbBirthDate,
                                            tbUsername, tbPassword, tbJMBG, tbPhoneNumber};
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                Utility.ShowWarning("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
                return;
            }

            CreatePatient();
            if (!_hospital.PatientService.CreateAccount(_patient))
                Utility.ShowWarning("Pacijent sa unetim _jmbg vec postoji");

            _record = null;
            _model.Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_patient is null)
            {
                Utility.ShowWarning("Nije selektovan nalog.");
                return;
            }

            MessageBoxResult result = Utility.ShowConfirmation("Da li ste sigurni da zelite da obrisete pacijenta?");
            if (result == MessageBoxResult.No)
                return;

            _patient = (Patient)lvPatients.SelectedItem;
            if (!_hospital.PatientService.DeleteAccount(_patient.JMBG))
                Utility.ShowWarning("Pacijent sa unetim _jmbg ne postoji");

            ClearBoxes();
            _model.Update();
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _patient = (Patient) lvPatients.SelectedItem;
            if (_patient is null) return; 

            tbName.Text = _patient.Name;
            tbLastName.Text = _patient.LastName;
            tbJMBG.Text = _patient.JMBG;
            tbAddress.Text = _patient.Address;
            tbPassword.Text = _patient.Password;
            tbPhoneNumber.Text = _patient.PhoneNumber;
            tbUsername.Text = _patient.UserName;
            tbBirthDate.Text = _patient.BirthDate.ToString();

            if (_patient.Gender == Gender.Male)
                cbMale.IsChecked = true;
            else cbFemale.IsChecked = true;

            if (_patient.Blocked == true)
                chbBlocked.IsChecked = true;
            else chbBlocked.IsChecked = false;

            _record = _patient.MedicalRecord;
        }

        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            new AddMedicalRecordView(this).ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                Utility.ShowWarning("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
                return;
            }

            CreatePatient();
            if(!_hospital.PatientService.UpdateAccount(_patient))
                Utility.ShowWarning("Pacijent sa unetim _jmbg ne postoji");
            _model.Update();
        }

        public void CreatePatient()
        {
            _patient = new Patient();
            _patient.Name = tbName.Text;
            _patient.LastName = tbLastName.Text;
            _patient.JMBG = tbJMBG.Text;
            _patient.BirthDate = DateTime.Parse(tbBirthDate.Text);
            _patient.PhoneNumber = tbPhoneNumber.Text;
            _patient.Address = tbAddress.Text;

            if (cbMale.IsChecked is bool Checked && Checked)
                _patient.Gender = Gender.Male;
            else _patient.Gender = Gender.Female;

            _patient.UserName = tbUsername.Text;
            _patient.Password = tbPassword.Text;

            if (chbBlocked.IsChecked is bool CheckedBlocked && CheckedBlocked)
                _patient.Blocked = true;
            else _patient.Blocked = false;
            if (_record is null)
                _record = new MedicalRecord();

            _patient.MedicalRecord = _record;
        }

        public void ClearBoxes()
        {
            foreach (var textBox in _textBoxes)
                textBox.Clear();
            _patient = null;
            _record = null;
        }

        public bool Validate()
        {
            return DateTime.TryParse(tbBirthDate.Text, out _) &&
                   _textBoxes.Count(x => x.Text.Trim() == "") == 0;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
        }
    }
}
