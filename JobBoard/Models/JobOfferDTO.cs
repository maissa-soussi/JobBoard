using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class JobOfferDTO
    {
        public JobOffer JobOffer { get; set; }
        public List<CandidatureDTO> CandidatureDTOs { get; set; }
    }
}
