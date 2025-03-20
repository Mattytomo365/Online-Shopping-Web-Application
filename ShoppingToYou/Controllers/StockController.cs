using Database.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingToYou.Models;

namespace ShoppingToYou.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly IStockRepo stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            this.stockRepo = stockRepo;
        }

        public IActionResult Index() //called when the staff hompage is about to be displayed
        {
            var lowStock = stockRepo.GetLowStock();

            return View(lowStock);
        }

        public IActionResult Manage() //called when manage stock button is clicked
        {
            return View("ManageStock");
        }

        // Loads the autocomplete search box
        [HttpGet("find")]
        public IActionResult Find()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var results = stockRepo.GetProductsDescriptionsBySearchTerm(term);

                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Called on selection from autocomplete 
        public IActionResult GetProducts(int id)
        {
            try
            {
                var results = stockRepo.GetProductsDetailsById(id);

                return PartialView("_ProductDetailsPartial", results);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("create")]
        public IActionResult Create() //called when add stock button is clicked
        {
            return PartialView("_CreateStockPartial");
        }

        [HttpPost("create")]
        public IActionResult Create(ProductDetails product)
        {

            product.ImageName = product.ImageFile.FileName;
            stockRepo.InsertProduct(product);

            return View("ManageStock");
        }

        // Loads the edit popup
        [HttpGet("Edit")]
        public IActionResult Edit(int id)
        {
            try
            {
                var results = stockRepo.GetProductsDetailsById(id);

                return PartialView("_EditStockPartial", results);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Saves changes made in edit popup
        [HttpPost("Edit")]
        public IActionResult Edit(ProductDetails product)
        {
            try
            {
                if (product.ImageFile != null)
                {
                    product.ImageName = product.ImageFile.FileName; 
                }
                else
                {
                    product.ImageName = "";
                }

                stockRepo.UpdateProduct(product);

                return View("ManageStock");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(int id) //called when delete button is clicked
        {
            try
            {
                stockRepo.DeleteProductById(id);

                return View("ManageStock");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult Increase(int id, int dftOrderQty) //called when order button is clicked
        {
            try
            {
                stockRepo.IncreaseProduct(id, dftOrderQty);
                var lowStock = stockRepo.GetLowStock();

                return View("Index", lowStock);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
