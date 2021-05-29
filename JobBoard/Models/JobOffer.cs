using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Models
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? DiplomaId { get; set; }
        public virtual Diploma Diploma { get; set; }
        public int? ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
        public int? ContratTypeId { get; set; }
        public virtual ContratType ContratType { get; set; }
        public string Description { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public int NbPositions { get; set; }
        public int ExperienceDuration { get; set; }
        public string PublicationDate { get; set; }
        public string ExpirationDate { get; set; }
        public int? DomainId { get; set; }
        public virtual Domain Domain { get; set; }
    }
}
