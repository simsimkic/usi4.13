using HealthCare.Model;

namespace HealthCare.Service
{
    public enum UserRole
    {
        Manager, Doctor, Nurse, Patient
    }

    public interface IUserService
    {
        public User? GetByUsername(string username);
        public UserRole GetRole();
    }
}
