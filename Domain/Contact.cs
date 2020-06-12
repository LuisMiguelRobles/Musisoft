

namespace Domain
{
    using System;
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser User { get; set; }

    }
}
