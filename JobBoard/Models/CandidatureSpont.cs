using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidatureSpont
    {
        public int Id { get; set; }
        public String JobInterviewDate { get; set; }
        public String CandidatureDate { get; set; }
        public string CoverLetterPath { get; set; }
        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
