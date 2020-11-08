using MedBook_RazorPages.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MedBook_RazorPages.Resources
{
    public class FilterOfRows
    {

        public Location getLocationWithId(List<Location> locations, int id)
        {
            return locations.Where(l => l.id == id).FirstOrDefault();
        }

        public List<MedicalService> getAllMedicalSerices(List<MedicalService> medicalService,
            List<Location> locations,
            QuerryDecorator querryDecorator)
        {
            List<MedicalService> retList = new List<MedicalService>();
            retList.Clear();
           
            foreach (MedicalService medServ in medicalService)
            {
                bool isValid = true;
                Location loc = locations.Where(loc => loc.id == medServ.LocationId).Single();
                if (querryDecorator.mCity != default(string) && !loc.City.Equals(querryDecorator.mCity))
                {  
                        isValid = false;
                }

                if (querryDecorator.mDescription != default(string) && !medServ.Description.Trim().ToLower().Contains(querryDecorator.mDescription.Trim().ToLower()))
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


        public List<MedicalService> getAllMedicalSerices(List<MedicalService> medicalService,
            QuerryDecorator querryDecorator)
        {
            List<MedicalService> retList = new List<MedicalService>();
            retList.Clear();

            foreach (MedicalService medServ in medicalService)
            {
                bool isValid = true;
                if (querryDecorator.mDescription != default(string) && !medServ.Description.Contains(querryDecorator.mDescription))
                {
                    isValid = false;
                }
                if (querryDecorator.mTargetBodySystem != default(string) && !medServ.TargetBodySystem.Contains(querryDecorator.mTargetBodySystem))
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

        public List<string> getAllMedicalSericesProviders()
        {
            throw new NotImplementedException();
        }


    }
}
