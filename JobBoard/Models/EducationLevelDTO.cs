using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class EducationLevelDTO
    {
        public EducationLevel EducationLevel { get; set; }
        public List<int?> CandidateIDs { get; set; }
    }
}
