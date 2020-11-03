using DocumentFormat.OpenXml.Presentation;
using MedBook_RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Resources
{
    public class QuerryDecorator
    {
        public string mQuerry { set; get; }
        public string mAppointmentType { set; get; }
        public string mCity { set; get; }
        public DateTime mLeftDateInterval { set; get; }
        public DateTime mRightDateInterval { set; get; }

        QuerryDecorator(string querry)
        {
            this.mQuerry = querry;
        }

        QuerryDecorator(string querry,
            string appointmentType)
        {
            this.mQuerry = querry;
            this.mAppointmentType = appointmentType;
        }
        QuerryDecorator(string querry,
            string appointmentType,
            string city)
        {
            this.mQuerry = querry;
            this.mAppointmentType = appointmentType;
            mCity = city;
        }

        public List<MedicalService> getListOfMedicalServices()
        {
            throw new NotImplementedException();
        }

    }
}
