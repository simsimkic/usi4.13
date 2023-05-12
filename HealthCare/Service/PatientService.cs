using HealthCare.Model;

namespace HealthCare.Service
{
    public class PatientService : Service<Patient>, IUserService
	{
		public PatientService(string filepath) : base(filepath) { }

		public bool CreateAccount(Patient newPatient)
		{
			if (!Contains(newPatient.JMBG))
			{
                Add(newPatient);
				return true;
            }
			return false;
		}

		public bool UpdateAccount(Patient updatedPatient)
		{
            if (Contains(updatedPatient.JMBG))
			{
				Update(updatedPatient);
				return true;
			}
			return false;
        }

		public bool DeleteAccount(string JMBG)
		{
			if (Contains(JMBG))
			{
                Remove(JMBG);
                return true;
			}
			return false;
        }

		public Patient? GetAccount(string JMBG)
		{
            return TryGet(JMBG);
		}

        public void UpdatePatientMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {
            patient.MedicalRecord = medicalRecord;
			Update(patient);
		}

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }

        public UserRole GetRole()
        {
            return UserRole.Patient;
        }
    }
}
