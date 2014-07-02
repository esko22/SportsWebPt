using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Clinic
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayImage { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        #endregion

        #region ForeignKeys
		 
	    #endregion

        #region Naviagation Properties

        public ICollection<Location> Locations { get; set; } 
        public ICollection<ClinicTherapistMatrixItem> ClinicTherapistMatrixItems { get; set; }
        public ICollection<ClinicAdminMatrixItem> ClinicAdminMatrixItems { get; set; }
        public ICollection<ClinicPatientMatrixItem> ClinicPatientMatrixItems { get; set; } 
		 
	    #endregion
    }

    public class Location
    {
        #region Properties

        public Int64 Id { get; set; }

        public String Address { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public int Zipcode { get; set; }

        #endregion

        #region Foreign Keys

        public int ClinicId { get; set; }

        #endregion
    }
}
