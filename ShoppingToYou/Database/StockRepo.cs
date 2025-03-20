using Database.Models;
using ShoppingToYou.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database.Repos
{
    public class StockRepo : IStockRepo
    {
        private readonly ISqlServerConnection sqlServerConnection;

        public StockRepo(ISqlServerConnection sqlServerConnection)
        {
            this.sqlServerConnection = sqlServerConnection ?? throw new ArgumentNullException(nameof(sqlServerConnection));
        }

        public List<LowStock> GetLowStock()
        {
            List<LowStock> lowStockList = new List<LowStock>();

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ProductMaster_GetLowStock", conn); //called to retrieve low stock
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {             
                while (reader.Read())
                {
                    var line = new LowStock
                    {
                        ID = reader.GetValueOrDefault<int>("ID"),
                        ProductCode = reader.GetValueOrDefault<string>("ProductCode"),
                        ProductDescription = reader.GetValueOrDefault<string>("ProductDescription"),
                        StockQuantity = reader.GetValueOrDefault<int>("StockQuantity"),
                        DefaultOrderQuantity = reader.GetValueOrDefault<int>("DefaultOrderQuantity")
                    };

                    lowStockList.Add(line);
                }

                return lowStockList;
            }
            else
            {
                return lowStockList;
            }
        }

        public List<Products> GetProductsTypesBySearchTerm(string searchTerm)
        {
            List<Products> products = new List<Products>();

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ProductTypeSearch", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var product = new Products
                    {
                        ProductType = reader.GetValueOrDefault<string>("ProductType")
                    };

                    products.Add(product);
                }

                return products;
            }
            else
            {
                return products;
            }
        }

        public List<ProductDetails> GetProductsDetailsByType(string productType)
        {
            List<ProductDetails> products = new List<ProductDetails>();

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ProductDetailsByType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductType", productType);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var product = new ProductDetails
                    {
                        ID = reader.GetValueOrDefault<int>("ID"),
                        ProductDescription = reader.GetValueOrDefault<string>("ProductDescription"),
                        ProductPrice = reader.GetValueOrDefault<double>("ProductPrice"),
                        ImageName = reader.GetValueOrDefault<string>("ImageName")
                    };

                    products.Add(product);
                }

                return products;
            }
            else
            {
                return products;
            }
        }


        public List<Products> GetProductsDescriptionsBySearchTerm(string searchTerm)
        {
            List<Products> products = new List<Products>();

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ProductDescriptionSearch", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var product = new Products
                    {
                        ProductID = reader.GetValueOrDefault<int>("ID"),
                        ProductDescription = reader.GetValueOrDefault<string>("ProductDescription")
                    };

                    products.Add(product);
                }

                return products;
            }
            else
            {
                return products;
            }
        }

        public ProductDetails GetProductsDetailsById(int id)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ProductDetailsById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                var product = new ProductDetails
                {
                    ID = reader.GetValueOrDefault<int>("ID"),
                    ProductDescription = reader.GetValueOrDefault<string>("ProductDescription"),
                    ProductPrice = reader.GetValueOrDefault<double>("ProductPrice"),
                    ImageName = reader.GetValueOrDefault<string>("ImageName"),
                    Quantity = reader.GetValueOrDefault<int>("StockQuantity"),
                    ReOrderQuantity = reader.GetValueOrDefault<int>("ReorderQuantity"),
                    DefaultOrderQuantity = reader.GetValueOrDefault<int>("DefaultOrderQuantity")
                };

                return product;
            }
            else
            {
                return null;
            }
        }

        public void UpdateProduct(ProductDetails product)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("UpdateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", product.ID);
            cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
            cmd.Parameters.AddWithValue("@ReOrderQuantity", product.ReOrderQuantity);
            cmd.Parameters.AddWithValue("@DefaultOrderQuantity", product.DefaultOrderQuantity);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            cmd.Parameters.AddWithValue("@StockQuantity", product.Quantity);
            cmd.Parameters.AddWithValue("@ImageName", product.ImageName);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException($"Failed To Update product ID {product.ID}");
            }
        }

        public void DeleteProductById(int id)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("DeleteProductById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException($"Failed To Delete product ID {id}");
            }
        }

        public void InsertProduct(ProductDetails product)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("InsertProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
            cmd.Parameters.AddWithValue("@StockQuantity", product.Quantity);
            cmd.Parameters.AddWithValue("@ReOrderQuantity", product.ReOrderQuantity);
            cmd.Parameters.AddWithValue("@DefaultOrderQuantity", product.DefaultOrderQuantity);
            cmd.Parameters.AddWithValue("@ProductType", product.ProductType);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            cmd.Parameters.AddWithValue("@ImageName", product.ImageName);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException("Failed To Insert product");
            }
        }

        public void IncreaseProduct(int id, int dftOrderQty)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("IncreaseLowStock", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@DefaultOrderQuantity", dftOrderQty);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException("Failed To Increase product");
            }
        }

    }
}
