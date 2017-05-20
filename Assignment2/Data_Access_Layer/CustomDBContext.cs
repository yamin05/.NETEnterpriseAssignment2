using Assignment2.Models.Database_Models;
using System.Data.Entity;

namespace Assignment2.Data_Access_Layer
{
    public class CustomDBContext : DbContext
    {
        public CustomDBContext() : base("name=Assignment2Database")
        {
            Database.SetInitializer(new CustomDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<InterventionType> InterventionTypes { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
    }
}