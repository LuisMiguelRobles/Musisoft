namespace Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
