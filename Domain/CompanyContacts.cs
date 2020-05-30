using System;

namespace Domain
{
    public class CompanyContacts
    {
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public Guid ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
