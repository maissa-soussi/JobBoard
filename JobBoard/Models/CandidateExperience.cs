using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidateExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Profession { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
        public int? DomainId { get; set; }
        public virtual Domain Domain { get; set; }
        public int? CandidateId { get; set; }
        //public virtual Candidate Candidate { get; set; }
    }
}
