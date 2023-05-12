using HealthCare.Context;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace HealthCare.View.AppointmentView
{
    public class PriorityAppointmentViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public Hospital _hospital;

        public PriorityAppointmentViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Doctors = new ObservableCollection<Doctor>();
            Appointments = new ObservableCollection<Appointment>();
            LoadDoctors(hospital.DoctorService.GetAll());
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            Doctors.Clear();
            foreach (Doctor doctor in doctors)
            {
                Doctors.Add(doctor);
            }
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            Appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                Appointments.Add(appointment);
            }
        }

        public void getAppointments(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor, string priority)
        {
            Appointment resultAppointment;
            if (priority=="Date")
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDate(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd);
                }
            }
            else
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                }
            }
            List<Appointment> appointments = new List<Appointment>();
            if (resultAppointment == null)
            {
                appointments = GetAppointmentByDoctor(hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
            }
            else
            {
                appointments.Add(resultAppointment);
            }
            LoadAppointments(appointments);
        }

        public Appointment GetAppointmentByDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            Patient patient = (Patient)_hospital.Current;
            while (startDate < endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (doctor.IsAvailable(timeSlot) && patient.IsAvailable(timeSlot))
                {
                    return new Appointment(patient, doctor, timeSlot, false);
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                }
            }
            return null;
        }

        public List<Appointment> GetAppointmentByDoctor(int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            List<Appointment> appointments = new List<Appointment>();
            Patient patient = (Patient)_hospital.Current;
            while (appointments.Count() < 3)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (doctor.IsAvailable(timeSlot) && patient.IsAvailable(timeSlot))
                {
                    appointments.Add(new Appointment(patient, doctor, timeSlot, false));
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                }
            }
            return appointments;
        }

        public Appointment GetAppointmentByDate(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd)
        {
            List<Doctor> doctors = _hospital.DoctorService.GetAll();
            foreach (Doctor doctor in doctors)
            {
                DateTime startDate = DateTime.Today;
                startDate = startDate.AddHours(hoursStart);
                startDate = startDate.AddMinutes(minutesStart);
                Patient patient = (Patient)_hospital.Current;
                while (startDate < endDate)
                {
                    TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                    if (patient.IsAvailable(timeSlot) && doctor.IsAvailable(timeSlot))
                    {
                        return new Appointment(patient, doctor, timeSlot, false);
                    }
                    startDate = startDate.AddMinutes(15);
                    if (startDate.Hour > hoursEnd)
                    {
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                    }
                }
            }
            return null;
        }

        public Appointment GetAppointmentByDateAndDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {

            DateTime startDate = DateTime.Today;
            startDate = startDate.AddHours(hoursStart);
            startDate = startDate.AddMinutes(minutesStart);
            Patient patient = (Patient)_hospital.Current;
            while (startDate < endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                if (patient.IsAvailable(timeSlot) && doctor.IsAvailable(timeSlot))
                {
                    return new Appointment(patient, doctor, timeSlot, false);
                }
                startDate = startDate.AddMinutes(15);
                if (startDate.Hour > hoursEnd)
                {
                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                }
            }

            return null;
        }
        public void IsUserBlocked()
        {
            Patient patient = (Patient)_hospital.Current;
            using (var reader = new StreamReader(Global.patientLogsPath, Encoding.Default))
            {
                string line;
                int updateDeleteCounter = 0;
                int createCounter = 0;
                while ((line = reader.ReadLine()) != null)
                {

                    string[] values = line.Split('|');
                    if (values[0] == patient.JMBG)
                    {
                        DateTime inputDate = DateTime.Parse(values[2]);
                        DateTime currentDate = DateTime.Now;
                        int daysDifference = (currentDate - inputDate).Days;
                        if (daysDifference < 30)
                        {
                            if (values[1] == "CREATE") createCounter++;
                            if (values[1] == "UPDATE" || values[1] == "DELETE") updateDeleteCounter++;
                        }
                    }


                }
                if (updateDeleteCounter >= 5 || createCounter > 8)
                {
                    patient.Blocked = true;
                }
                else
                {
                    patient.Blocked = false;
                }
                _hospital.PatientService.UpdateAccount(patient);
            }
        }

    }
}
