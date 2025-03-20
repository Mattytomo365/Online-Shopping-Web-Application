using Database;
using Database.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingToYou.Database;
using ShoppingToYou.Models;
using System;
using System.Collections.Generic;

namespace ShoppingToYou.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IStockRepo stockRepo;
        private readonly IOrderRepo orderRepo;
        private readonly ICustomerRepo customerRepo;

        public OrderController(IStockRepo stockRepo, IOrderRepo orderRepo, ICustomerRepo customerRepo)
        {
            this.stockRepo = stockRepo;
            this.orderRepo = orderRepo;
            this.customerRepo = customerRepo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int custID;

            
            var userEmail = User.Identity.GetEmail(); //retrieves email address from tokens/claims
            custID = customerRepo.CheckCustomerByEmail(userEmail); //calls customer repo
            if (custID == 0) 
            {
                var customer = new Customer { Email = userEmail };
                return PartialView("CustomerDetailsPartial", customer); //calls partial view for customer to enter their address etc..
            }

            var orderNumber = orderRepo.GetOpenOrderNumberByCustomer(custID);//carries on with normal program and loads homepage
            var currentOrders = orderRepo.GetOrderLines(orderNumber);
            var results = currentOrders == null ? new List<OrderLines>() : currentOrders;

            ViewBag.CustomerId = custID;
            ViewBag.OrderNumber = orderNumber;
            return View(results);            
        }

        // Loads the autocomplete search box
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var results = stockRepo.GetProductsTypesBySearchTerm(term);

                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Displays product list based on selection from autocomplete 
        public IActionResult GetProducts(string term, string custId)
        {
            try
            {
                var results = stockRepo.GetProductsDetailsByType(term);

                ViewBag.CustomerId = custId;
                return View("ProductDetails", results);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Called from the order button
        public IActionResult Place(int customerId, int productId, int quantity)
        {
            try
            {
                // Check for an open order for this customer - if found return the order number else create it
                string orderNumber = orderRepo.GetOrCreateOrder(1);
                if (string.IsNullOrEmpty(orderNumber))
                    return BadRequest();

                // add line to the order
                orderRepo.InsertOrderLines(orderNumber, quantity, productId);
                ViewBag.CustomerId = customerId;
                ViewBag.OrderNumber = orderNumber;

                var currentOrder = orderRepo.GetOrderLines(orderNumber);
                return View("index", currentOrder);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Called from the finalise button
        public IActionResult Delivery(string orderNumber)
        {
            try
            {
                orderRepo.MarkOrderAsOrdered(orderNumber);

                return View("OrderThanks");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult CreateCustomer(Customer customer) //called when submit button is clicked on the CustomerDetails partial view
        {
            try
            {
                var  custID = customerRepo.InsertCustomer(customer.Email, customer.FirstName, customer.Surname, customer.Address1, customer.Address2,
                                            customer.Address3, customer.Address4, customer.Postcode, customer.Phone); //calls new action

                var orderNumber = orderRepo.GetOpenOrderNumberByCustomer(custID);//carries on with normal program and loads homepage
                var currentOrders = orderRepo.GetOrderLines(orderNumber);
                var results = currentOrders == null ? new List<OrderLines>() : currentOrders;

                ViewBag.CustomerId = custID;
                ViewBag.OrderNumber = orderNumber;
                return View("index", results);
            }
            catch
            {
                return BadRequest();
            }                      
        }

        // Loads the edit popup
        [HttpGet("EditOrder")]
        public IActionResult EditOrder(int id, string custId)
        {
            try
            {
                var results = orderRepo.OrderLineById(id);
                results.CustomerId = Convert.ToInt32(custId);

                return PartialView("EditOrderPartial", results);

            }
            catch
            {
                return BadRequest();
            }
        }

        // Saves changes made in edit popup
        [HttpPost("EditOrder")]
        public IActionResult EditOrder(OrderLineDetail order)
        {
            try
            {
                orderRepo.UpdateOrder(order);
                var orderNumber = orderRepo.GetOpenOrderNumberByCustomer(order.CustomerId);//carries on with normal program and loads homepage
                var currentOrders = orderRepo.GetOrderLines(orderNumber);
                ViewBag.OrderNumber = orderNumber;
                ViewBag.CustomerId = order.CustomerId;
                return View("index", currentOrders);
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult DeleteOrder(int id, string custId)
        {
            try
            {
                orderRepo.DeleteOrder(id);

                var orderNumber = orderRepo.GetOpenOrderNumberByCustomer(Convert.ToInt32(custId));//carries on with normal program and loads homepage
                var currentOrders = orderRepo.GetOrderLines(orderNumber);
                ViewBag.OrderNumber = orderNumber;
                ViewBag.CustomerId = custId;
                return View("index", currentOrders);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
