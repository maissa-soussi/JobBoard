using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string PicturePath { get; set; }
        public string CvPath { get; set; }
        public string Phone { get; set; }
        public string BirthdayDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public int SalaryWishId { get; set; }
        public virtual SalaryWish SalaryWish { get; set; }
        public int DrivingLicenceId { get; set; }
        public virtual DrivingLicence DrivingLicence { get; set; }
    }
}
