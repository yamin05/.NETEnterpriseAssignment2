using Assignment2.Models;
using System.Threading.Tasks;

namespace Assignment2.Data_Access_Layer
{
    public class ProjectDBContext
    {
        private ApplicationDbContext identityDBContext { get; set; }
        private CustomDBContext customDBContext { get; set; }

        public ProjectDBContext()
        {
            identityDBContext = new ApplicationDbContext();
            customDBContext = new CustomDBContext();
        }

        public void Seed()
        {
            if (!identityDBContext.Database.Exists() && !customDBContext.Database.Exists())
            {
                var initializeIdentity = Task.Factory.StartNew(() => identityDBContext.Database.Initialize(true));
                initializeIdentity.Wait();
                var initializeCustom = Task.Factory.StartNew(() => customDBContext.Database.Initialize(true));
                initializeCustom.Wait();
            }
        }
    }
}