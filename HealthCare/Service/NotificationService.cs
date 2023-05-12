using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class NotificationService : NumericService<Notification>
    {
        public NotificationService(string filepath) : base(filepath) { }

        public List<Notification> GetForUser(string userJmbg)
        {
            return GetAll().FindAll(x => 
                !x.Seen && x.Recipients.Contains(userJmbg));
        }
    }
}
