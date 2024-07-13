using System.ComponentModel.DataAnnotations;

namespace Pinewood.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Addr1 { get; set; }

        public string Addr2 { get; set; }

        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
    }

}
