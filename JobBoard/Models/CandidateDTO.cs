using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidateDTO
    {
        public int? ExperienceId { get; set; }
        public int DrivingLicenceId { get; set; }
        public int SalaryWishId { get; set; }
        public string SalaryWish { get; set; }
        public List<CandidateLanguageDTO> Languages { get; set; }
    }
}
