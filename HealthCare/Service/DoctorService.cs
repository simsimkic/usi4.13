using HealthCare.Model;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class DoctorService : Service<Doctor>, IUserService
    {
        public DoctorService(string filePath) : base(filePath) { }  

        public Doctor GetAccount(string JMBG)
        {
            return Get(JMBG);
        }
        public List<Patient> GetExaminedPatients(Doctor doctor)
        {
            HashSet<Patient> patients = new HashSet<Patient>();
            foreach (var appointment in Schedule.Appointments)
            {
                if (appointment.Doctor.Equals(doctor))
                {
                    patients.Add(appointment.Patient);
                }
            }
            return patients.ToList();
        }

        public List<Doctor> GetAccounts() 
        {
            return GetAll();
        }

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }

        public List<Doctor> GetBySpecialization(string specialization)
        {
            return GetAll().Where(x => x.IsCapable(specialization)).ToList();
        }

        public List<string> GetSpecializations()
        {
            return GetAll().Select(x => x.Specialization).Distinct().ToList();
        }

        public UserRole GetRole()
        {
            return UserRole.Doctor;
        }
    }
}
