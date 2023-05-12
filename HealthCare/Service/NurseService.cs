using HealthCare.Model;

namespace HealthCare.Service
{
    public class NurseService : Service<User>, IUserService
    {
        public NurseService(string filepath) : base(filepath) { }

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }

        public UserRole GetRole()
        {
            return UserRole.Nurse;
        }
    }
}
