using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class PdfGeneratorModel
    {
        public Career CareerData { get; set; }

        public Contact ContactData { get; set; }

        public Education EducationData { get; set; }

        public List<Family> FamilyData { get; set; }

        public Personal PersonalData { get; set; }

        public Religious ReligiousData { get; set; }

        public PictureModel ProfilePicture { get; set; }

        public List<PictureModel> PictureListData { get; set; }
    }
}