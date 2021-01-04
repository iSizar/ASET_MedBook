using MedBook_RazorPages.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Resources
{
    public class DBDataAccess : IDataAccess
    {
        DatabaseContext _db;

        public DBDataAccess(DatabaseContext db)
        {
            this._db = db;
        }

        public List<Appointment> getAppointment()
        {
            return _db.Appointment.ToList();
        }

        public List<Location> getLocation()
        {
            return _db.Location.ToList();
        }


        public List<MedicalService> getMedicalService()
        {
            List<MedicalService> retList = new List<MedicalService>();
            foreach (MedicalService medServ in _db.MedicalService.ToList())
            {
                _db.Entry(medServ).Reference(m => m.Specialization).Load();
                _db.Entry(medServ).Reference(m => m.Location).Load();
                retList.Add(medServ);
            }
            return retList;
        }

        public List<Review> getReview()
        {
            return _db.Review.ToList();
        }

        public List<Users> getUsers()
        {
            return _db.Users.ToList();
        }
    }
}
