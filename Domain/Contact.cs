

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
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }


    }
}
