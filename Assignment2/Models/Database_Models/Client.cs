using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Database_Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientLocation { get; set; }
        public int ClientDistrict { get; set; }
    }
}