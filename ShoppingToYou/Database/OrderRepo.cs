using Database;
using ShoppingToYou.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingToYou.Database
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ISqlServerConnection sqlServerConnection;

        public OrderRepo(ISqlServerConnection sqlServerConnection)
        {
            this.sqlServerConnection = sqlServerConnection ?? throw new ArgumentNullException(nameof(sqlServerConnection));
        }

        public string GetOpenOrderNumberByCustomer(int customerId)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("GetOpenOrderNumberByCustomer", conn); //called to display any items in basket on customer homepage 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                return reader.GetValueOrDefault<string>("OrderNumber");

            }
            else
            {
                return string.Empty;
            }
        }

        public string GetOrCreateOrder(int customerId)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("GetOrCreateOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                return reader.GetValueOrDefault<string>("OrderNumber");

            }
            else
            {
                return null;
            }
        }

        public List<OrderLines> GetOrderLines(string orderNumber)
        {
            List<OrderLines> OrderLine = new List<OrderLines>();

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("GetShoppingList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    var line = new OrderLines
                    {
                        ID = reader.GetValueOrDefault<int>("ID"),
                        OrderNumber = reader.GetValueOrDefault<string>("OrderNumber"),
                        OrderLine = reader.GetValueOrDefault<int>("OrderLine"),
                        ProductID = reader.GetValueOrDefault<int>("ProductID"),
                        OrderQuantity = reader.GetValueOrDefault<int>("OrderQuantity"),
                        ProductDescription = reader.GetValueOrDefault<string>("ProductDescription"),
                        ProductPrice = reader.GetValueOrDefault <double>("ProductPrice"),
                        ImageName = reader.GetValueOrDefault<string>("ImageName")
                    };

                    OrderLine.Add(line);
                }

                return OrderLine;
            }
            else
            {
                return OrderLine;
            }
        }


        public void InsertOrderLines(string orderNumber, int orderQuantity, int productId)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("InsertOrderLines", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
            cmd.Parameters.AddWithValue("@OrderQuantity", orderQuantity);
            cmd.Parameters.AddWithValue("@ProductID", productId);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException($"Failed To Insert Order Line for Order {orderNumber}");
            }
        }

        public void MarkOrderAsOrdered(string orderNumber)
        {
            if (orderNumber is null)
            {
                throw new ArgumentNullException(nameof(orderNumber));
            }

            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("MarkOrderAsOrdered", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException($"Failed To Update Order {orderNumber}");
            }
        }

        public void UpdateOrder(OrderLineDetail order) 
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("UpdateOrder", conn); //called when save button is clicked on the edit order popup
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", order.ID);
            cmd.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException( "Failed To Update order");
            }
        }

       public void DeleteOrder(int id)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new("DeleteOrder", conn); //called when delete button is clicked
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);

            // execute query
            int numberRecordsAffected = cmd.ExecuteNonQuery();

            
            if (numberRecordsAffected <= 0)
            {
                throw new ApplicationException("Failed To Delete Order");
            }
        }

        public OrderLineDetail OrderLineById(int id)
        {
            using SqlConnection conn = sqlServerConnection.GetConnection;
            conn.Open();

            using SqlCommand cmd = new SqlCommand("OrderLineById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);

            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                var orderLine = new OrderLineDetail
                {
                    ID = reader.GetValueOrDefault<int>("ID"),
                    ProductDescription = reader.GetValueOrDefault<string>("ProductDescription"),
                    ProductPrice = reader.GetValueOrDefault<double>("ProductPrice"),
                    OrderQuantity = reader.GetValueOrDefault<int>("OrderQuantity")
                };
                return orderLine;
            }
            else
            {
                return null;
            }
        }
    }
}
