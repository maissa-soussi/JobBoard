using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class Other
    {
        public int Id { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public int SalaryWishId { get; set; }
        public virtual SalaryWish SalaryWish { get; set; }
        public int DrivingLicenceId { get; set; }
        public virtual DrivingLicence DrivingLicence { get; set; }
    }
}
