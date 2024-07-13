using System.ComponentModel.DataAnnotations;

namespace Pinewood.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Addr1 { get; set; }
        
        public string Addr2 { get; set; }
        
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
