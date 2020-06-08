namespace Persistence
{
    using Domain;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Company>(x => x.HasKey(c => c.Id));
            builder.Entity<Campaign>(x => x.HasKey(c => c.Id));
            builder.Entity<Contact>(x => x.HasKey(c => c.Id));

            builder.Entity<AppUser>()
                .HasMany(u => u.Companies)
                .WithOne(c => c.User);

            builder.Entity<Company>()
                .HasMany(c => c.Campaigns)
                .WithOne(cmp => cmp.Company)
                .HasForeignKey(c => c.CompanyId);


            builder.Entity<Company>()
                .HasMany(cn => cn.Contacts)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId);
        }

    }
}