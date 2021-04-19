using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidatureDTO
    {
        public String JobInterviewDate { get; set; }
        public String CandidatureDate { get; set; }
        public string CoverLetterPath { get; set; }
        public int? JobOfferId { get; set; }
        public Candidate Candidate { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<CandidateLanguageDTO> Languages { get; set; }
        public List<Diploma> Diplomas { get; set; }
    }
}
