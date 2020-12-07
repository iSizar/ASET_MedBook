using MedBook_RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Resources
{
    interface IDataAccess
    {
        public List<Users> getUsers();

        public List<MedicalService> getMedicalService();
        public List<MedicalService> getMedicalService(QuerryDecorator qd = null);
        public List<Review> getReview();
        public List<Location> getLocation();
        public List<Appointment> getAppointment();
    }
}
