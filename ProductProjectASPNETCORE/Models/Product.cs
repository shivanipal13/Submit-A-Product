using System.ComponentModel.DataAnnotations;

namespace ProductProjectASPNETCORE.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required]
        public string productName { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        [Required]
        public string Productfreshness { get; set; }
        [Required]
        public string ImageOfProduct { get; set; }

        public  string AdditionalDescription {get; set;}
        [Required]
        [Range(1,double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "MRP must be greater than zero.")]
        public decimal ProductMRP { get; set; }
        [Required]
        [EmailAddress]
        public string ManagerEmail { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter a valid 10-digit phone number.")]
        public int ManagerPhoneNumber { get; set; }


    }
}
