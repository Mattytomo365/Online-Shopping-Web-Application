using ShoppingToYou.Models;
using System.Collections.Generic;

namespace ShoppingToYou.Database
{
    public interface IOrderRepo
    {
        string GetOpenOrderNumberByCustomer(int customerId);
        string GetOrCreateOrder(int customerId);
        List<OrderLines> GetOrderLines(string orderNumber);
        void InsertOrderLines(string orderNumber, int orderQuantity, int productId);
        void MarkOrderAsOrdered(string orderNumber);
        void UpdateOrder(OrderLineDetail order);
        public void DeleteOrder(int id);
        public OrderLineDetail OrderLineById(int id);
    }
}