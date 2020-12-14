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
                retList.Add(medServ);
            }
            return retList;
        }
        public List<MedicalService> getMedicalService(QuerryDecorator qd)
        {
            List<MedicalService> retList = new List<MedicalService>();
            retList.Clear();
            bool isValid;

            foreach (MedicalService medServ in _db.MedicalService.ToList().ToList())
            {
                isValid = true;
                _db.Entry(medServ).Reference(m => m.Location).Load();

                if (qd.mDescription != default && !medServ.Description.ToLower().Contains(qd.mDescription.ToLower()))
                {
                    isValid = false;
                }
                if (qd.mSpecialization != default && !medServ.Specialization.Description.Contains(qd.mSpecialization))
                {
                    isValid = false;
                }
                if (qd.mCity != default && medServ.Location.City.ToLower().Contains(qd.mCity.ToLower()))
                {
                    isValid = false;
                }
                if (qd.mMinRating != default &&
                    _db.Review.Where(r => r.MedicalServiceId == medServ.id).Count() > 0 &&
                    qd.mMinRating > _db.Review.Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating))
                {
                    isValid = false;
                }
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
