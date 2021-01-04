using MedBook_RazorPages.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MedBook_RazorPages.Resources
{
    public class FilterOfRows
    {
        private IDataAccess dataAccess;

        public FilterOfRows(IDataAccess dataAccess) => this.dataAccess = dataAccess;
       
        public List<MedicalService> getMedicalService(QuerryDecorator qd)
        {
            List<MedicalService> retList = new List<MedicalService>();
            retList.Clear();
            bool isValid;

            foreach (MedicalService medServ in dataAccess.getMedicalService())
            {
                isValid = true;

                if (qd.mDescription != default && !medServ.Description.ToLower().Contains(qd.mDescription.ToLower()))
                {
                    isValid = false;
                }
                if (qd.mSpecialization != default && !medServ.Specialization.Description.Contains(qd.mSpecialization))
                {
                    isValid = false;
                }
                if (qd.mCity != default && !medServ.Location.City.ToLower().Contains(qd.mCity.ToLower()))
                {
                    isValid = false;
                }
                if (qd.mMinRating != default &&
                    dataAccess.getReview().Where(r => r.MedicalServiceId == medServ.id).Count() > 0 &&
                    qd.mMinRating > dataAccess.getReview().Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating))
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

        public List<Users> getUsers()
        {
            return dataAccess.getUsers();
        }

        public List<MedicalService> getMedicalService() 
        {
            return dataAccess.getMedicalService();
        }
        public List<Review> getReview()
        {
            return dataAccess.getReview();
        }
        public List<Location> getLocation()
        {
            return dataAccess.getLocation();
        }
        public List<Appointment> getAppointment()
        {
            return dataAccess.getAppointment();
        }
    }
}
