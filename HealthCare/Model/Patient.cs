using HealthCare.Repository;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class Patient : User, ISerializable
    {
        public bool Blocked { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }

        public Patient(string name, string lastName, string jMBG, DateTime birthDate, string phoneNumber,
            string address, string userName, string password, Gender gender, bool blocked, MedicalRecord? medicalRecord) 
            : base(name, lastName, jMBG, birthDate, phoneNumber, address, userName,password,gender)
        {
            Blocked = blocked;
            MedicalRecord = medicalRecord;
        }
        
        public Patient() { }

        public bool IsAvailable(TimeSlot term)
        {
            List<Appointment> PatientAppointments = Schedule.GetPatientAppointments(this);
            foreach (Appointment appointment in PatientAppointments)
            {
                if (appointment.TimeSlot.Overlaps(term))
                {
                    return false;
                }
            }
            return true;
        }

        public new string[] ToCSV()
        {
            string[] userValues = base.ToCSV();
            string[] patientValues = {Blocked.ToString()};
            string[] medicalRecordValues = {""};
            if (MedicalRecord != null)
                medicalRecordValues = MedicalRecord.ToCSV();
            string[] csvValues = userValues.Concat(patientValues).Concat(medicalRecordValues).ToArray();
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Name = values[0];
            LastName = values[1];
            JMBG = values[2];
            BirthDate = Utility.ParseDate(values[3]);
            PhoneNumber = values[4];
            Address = values[5];
            UserName = values[6];
            Password = values[7];
            Gender = Utility.Parse<Gender>(values[8]);

            Blocked = bool.Parse(values[9]);
            MedicalRecord = new MedicalRecord();
            MedicalRecord.FromCSV(Utility.SubArray(values, 10, 4));
        }
    }
}
