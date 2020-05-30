

namespace Domain
{
    using System.Collections.Generic;
    using System;
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<CompanyContacts> CompanyContacts { get; set; }
    }
}
