using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class Candidature
    {
        public int Id { get; set; }
        public String JobInterviewDate { get; set; }
        public String CandidatureDate { get; set; }
       
        public string CoverLetterPath { get; set; }
        public int? JobOfferId { get; set; }
        public virtual JobOffer JobOffer { get; set; }
        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
