using Assignment2.Models.Database_Models;
using System.Data.Entity;

namespace Assignment2.Data_Access_Layer
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext() : base("name=Assignment2Database")
        {
            Database.SetInitializer(new ProjectDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}