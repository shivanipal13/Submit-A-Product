namespace ProductProjectASPNETCORE.Models
{
    public class ProductViewModel
    {

        public string productName { get; set; }
        public string ProductCategory { get; set; }
        public string Productfreshness { get; set; }
        public IFormFile ImageOfProduct { get; set; }
        public string AdditionalDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductMRP { get; set; }
        public string ManagerEmail { get; set; }
        public int ManagerPhoneNumber { get; set; }
    }
}
