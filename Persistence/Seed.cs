namespace Persistence
{
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                    },new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                    }
                };

                foreach (var user in users)
                {
                   await userManager.CreateAsync(user,"Pa$$w0rd");
                }
            }

            if (!context.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Past Activity 1",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "Drinks",
                        City = "London",
                        Venue = "Pub"
                    },
                    new Activity
                    {
                        Title = "Past Activity 2",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 months ago",
                        Category = "Culture",
                        City = "Paris",
                        Venue = "Louvre"
                    }
                };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }


            if (!context.Contacts.Any())
            {
                var contacts = new List<Contact>
                {
                    new Contact
                    {
                        Name = "Contact 1",
                        LastName = "Contact 1",
                        Email = "contact1@contact.com"
                        
                    },
                    new Contact
                    {
                        Name = "Contact 2",
                        LastName = "Contact 3",
                        Email = "contact1@contact.com"

                    }
                };
                await context.Contacts.AddRangeAsync(contacts);
                await context.SaveChangesAsync();
            }

            if (!context.Companies.Any())
            {
                var companies = new List<Company>
                {
                    new Company
                    {
                        AppUserId = "7dac390d-58fb-40b7-b69e-e70d37186f5d",
                        Name = "Company 1",
                        Nit = "123456"
                    },
                    new Company
                    {
                        AppUserId = "7aeb9416-686a-41b1-9ba8-d150caf5fb36",
                        Name = "Company 2",
                        Nit = "654321"
                    }
                };
                await context.Companies.AddRangeAsync(companies);
                await context.SaveChangesAsync();
            }

            if (!context.Campaigns.Any())
            {
                var campaigns = new List<Campaign>
                {
                 new Campaign
                 {
                     Name = "Campaign 1",
                     Description = "Campaign 1",
                     StartDate = DateTime.Now.AddDays(5),
                     EndDate = DateTime.Now.AddDays(10),
                     CompanyId = new Guid("7E34F1D3-6179-4CCC-9E92-C16C35C3A2B8")
                 },
                 new Campaign
                 {
                     Name = "Campaign 2",
                     Description = "Campaign 2",
                     StartDate = DateTime.Now.AddDays(5),
                     EndDate = DateTime.Now.AddDays(10),
                     CompanyId = new Guid("FAEFA7F1-3342-47B0-919C-47F64D8C8E7E")
                 }
                };
                await context.Campaigns.AddRangeAsync(campaigns);
                await context.SaveChangesAsync();
            }
        }
    }
}
