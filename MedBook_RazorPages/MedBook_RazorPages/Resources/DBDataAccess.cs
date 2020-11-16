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
            return _db.MedicalService.ToList();
        }
        public List<MedicalService> getMedicalService(QuerryDecorator qd)
        {
            List<MedicalService> retList = new List<MedicalService>();
            retList.Clear();
            bool isValid;

            foreach (MedicalService medServ in _db.MedicalService.ToList().ToList())
            {
                isValid = true;
                if (qd.mDescription != default(string) && !medServ.Description.Contains(qd.mDescription))
                {
                    isValid = false;
                }
                if (qd.mTargetBodySystem != default(string) && !medServ.TargetBodySystem.Contains(qd.mTargetBodySystem))
                {
                    isValid = false;
                }
                /*if (qd.mCity != default(string) && !medServ.Location.City.Contains(qd.mCity))
                {
                    isValid = false;
                }*/
                if (isValid)
                {
                    retList.Add(medServ);
                }
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
