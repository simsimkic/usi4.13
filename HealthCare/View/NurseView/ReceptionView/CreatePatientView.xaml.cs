using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.PatientView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.ReceptionView
{
    public partial class CreatePatientView : Window
    {
        private Hospital _hospital;
        public MedicalRecord? _record;
        private string _jmbg;
        private List<TextBox> _textBoxes;
        public CreatePatientView(Hospital hospital, string jmbg)
        {
            InitializeComponent();

            _hospital = hospital;
            _record = null;
            _jmbg = jmbg;

            tbJMBG.IsEnabled = false;
            tbJMBG.Text = jmbg;

            _textBoxes = new List<TextBox> { tbName, tbLastName, tbAddress, tbBirthDate,
                                            tbUsername, tbPassword, tbJMBG, tbPhoneNumber};
        }

        private void btnMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            new AddMedicalRecordView(this).ShowDialog();
        }
        public bool Validate()
        {
            return DateTime.TryParse(tbBirthDate.Text, out _) &&
                   _textBoxes.Count(x => x.Text.Trim() == "") == 0;
        }

        public Patient CreatePatient()
        {
            Patient patient = new Patient();
            patient.Name = tbName.Text;
            patient.LastName = tbLastName.Text;
            patient.JMBG = _jmbg;
            patient.BirthDate = DateTime.Parse(tbBirthDate.Text);
            patient.PhoneNumber = tbPhoneNumber.Text;
            patient.Address = tbAddress.Text;

            if (cbMale.IsChecked is bool Checked && Checked)
                patient.Gender = Gender.Male;
            else patient.Gender = Gender.Female;

            patient.UserName = tbUsername.Text;
            patient.Password = tbPassword.Text;

            if (chbBlocked.IsChecked is bool CheckedBlocked && CheckedBlocked)
                patient.Blocked = true;
            else patient.Blocked = false;

            if (_record is null)
                _record = new MedicalRecord();
            patient.MedicalRecord = _record;

            return patient;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                Utility.ShowWarning("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
                return;
            }

            Patient patient = CreatePatient();
            if (!_hospital.PatientService.CreateAccount(patient))
                Utility.ShowWarning("Pacijent sa unetim _jmbg vec postoji");

            _record = null;
            Close();
        }
    }
}
