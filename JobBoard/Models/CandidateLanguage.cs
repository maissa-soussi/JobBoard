using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidateLanguage
    {
        public int Id { get; set; }
        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public int? LanguageLevelId { get; set; }
        public virtual LanguageLevel LanguageLevel { get; set; }
    }
}
