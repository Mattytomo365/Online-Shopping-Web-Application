using Database.Models;
using ShoppingToYou.Models;
using System.Collections.Generic;

namespace Database.Repos
{
    public interface IStockRepo
    {
        public List<LowStock> GetLowStock();
        List<Products> GetProductsTypesBySearchTerm(string searchTerm);
        List<ProductDetails> GetProductsDetailsByType(string productType);
        List<Products> GetProductsDescriptionsBySearchTerm(string searchTerm);
        ProductDetails GetProductsDetailsById(int id);
        void UpdateProduct(ProductDetails product);
        void DeleteProductById(int id);
        void InsertProduct(ProductDetails product);
        void IncreaseProduct(int id, int dftOrderQty);
    }
}
