using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductProjectASPNETCORE.Data;
using ProductProjectASPNETCORE.Models;

namespace ProductProjectASPNETCORE.Controllers
{
    public class ProductController : Controller
    {
        public readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
            
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Product product , [FromForm]IFormFile ImageOfProduct)
        //{
        //    if(_context.Products.Count() >=10)
        //    {
        //        ModelState.AddModelError(string.Empty, "Cannot add more than 10 products.");
        //        return View(product);
        //    }

        //    if (product.ProductMRP < product.ProductPrice)
        //    {
        //        ModelState.AddModelError(nameof(product.ProductMRP), "MRP cannot be less than Price.");
        //    }

        //    if (ImageOfProduct != null && ImageOfProduct.Length > 0)
        //    {
        //        // Save the image to the server
        //        string fileName = Path.GetFileName(ImageOfProduct.FileName);
        //        string filePath = Path.Combine("wwwroot/images", fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await ImageOfProduct.CopyToAsync(stream);
        //        }

        //        product.ImageOfProduct = $"/images/{fileName}"; // Store relative path in the database
        //    }
        //    else
        //    {
        //        // Handle the case where the image is not provided
        //        ModelState.AddModelError("ImageOfProduct", "The image is required.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productviewmodel, [FromForm] IFormFile ImageOfProduct)
        {
            if (_context.Products.Count() >= 10)
            {
                ModelState.AddModelError(string.Empty, "Cannot add more than 10 products.");
                return View(productviewmodel);
            }

            if (productviewmodel.ProductMRP < productviewmodel.ProductPrice)
            {
                ModelState.AddModelError(nameof(productviewmodel.ProductMRP), "MRP cannot be less than Price.");
            }

            string filePath = null;

            if (productviewmodel.ImageOfProduct != null && productviewmodel.ImageOfProduct.Length > 0)
            {
                string fileName = Path.GetFileName(productviewmodel.ImageOfProduct.FileName);
                filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productviewmodel.ImageOfProduct.CopyToAsync(stream);
                }

                filePath = $"/images/{fileName}";
            }
            else
            {
                ModelState.AddModelError("ImageOfProduct", "The image is required.");
            }

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    productName = productviewmodel.productName,
                    ProductCategory = productviewmodel.ProductCategory,
                    Productfreshness = productviewmodel.Productfreshness,
                    ImageOfProduct = filePath, // Store the file path
                    AdditionalDescription = productviewmodel.AdditionalDescription,
                    ProductPrice = productviewmodel.ProductPrice,
                    ProductMRP = productviewmodel.ProductMRP,
                    ManagerEmail = productviewmodel.ManagerEmail,
                    ManagerPhoneNumber = productviewmodel.ManagerPhoneNumber
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productviewmodel);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productviewmodel)
        {
            // Fetch the existing product from the database
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Validate MRP cannot be less than Price
            if (productviewmodel.ProductMRP < productviewmodel.ProductPrice)
            {
                ModelState.AddModelError(nameof(productviewmodel.ProductMRP), "MRP cannot be less than Price.");
            }

            string filePath = product.ImageOfProduct; // Default to existing image path

            // Handle Image Upload
            if (productviewmodel.ImageOfProduct != null && productviewmodel.ImageOfProduct.Length > 0)
            {
                string fileName = Path.GetFileName(productviewmodel.ImageOfProduct.FileName);
                filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productviewmodel.ImageOfProduct.CopyToAsync(stream);
                }

                filePath = $"/images/{fileName}"; // Update to new file path
            }

            if (ModelState.IsValid)
            {
                // Update the product with new details
                product.productName = productviewmodel.productName;
                product.ProductCategory = productviewmodel.ProductCategory;
                product.Productfreshness = productviewmodel.Productfreshness;
                product.ImageOfProduct = filePath; // Store the updated file path
                product.AdditionalDescription = productviewmodel.AdditionalDescription;
                product.ProductPrice = productviewmodel.ProductPrice;
                product.ProductMRP = productviewmodel.ProductMRP;
                product.ManagerEmail = productviewmodel.ManagerEmail;
                product.ManagerPhoneNumber = productviewmodel.ManagerPhoneNumber;

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productviewmodel); // Return the view model with validation errors
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Where(a => a.Id == id).FirstOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
