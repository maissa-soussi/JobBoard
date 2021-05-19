using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class CandidateDiploma
    {
		public int Id { get; set; }
		
		public string Date { get; set; }

		public string University { get; set; }
		
		public int? CandidateId { get; set; }
		public virtual Candidate Candidate { get; set; }
		
		public int? DomainId { get; set; }
		public virtual Domain Domain { get; set; }

		public int? DiplomaId { get; set; }
		public virtual Diploma Diploma { get; set; }

		public int? EducationLevelId { get; set; }
		public virtual EducationLevel EducationLevel { get; set; }
	}
}
