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
        public string mDescription{ set; get; }
        public string mTargetBodySystem { set; get; }
        public string mCity { set; get; }
        public DateTime mLeftDateInterval { set; get; }
        public DateTime mRightDateInterval { set; get; }

        public QuerryDecorator()
        {
        }

    }
}
