namespace Domain
{
    using System.Collections.Generic;
    using System;
    public class Company
    {
        public Guid Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser User { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
