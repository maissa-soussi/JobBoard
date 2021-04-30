using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class JobOfferCandidature
    {
        public int? CandidatureId { get; set; }
        public String JobInterviewDate { get; set; }
        public String CandidatureDate { get; set; }
        public string CoverLetterPath { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
    }
}
