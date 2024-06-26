using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.OrdersView
{
    public class DetailsModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public DetailsModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        public Orders Orders { get; set; } = default!;
        public List<OrderDetails> OrderDetails { get; set; } = default!;
        public string? CustomerName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders != null)
            {
                CustomerName = _context.Customers.Where(cus => cus.CustomerId == orders.CustomerId)
                                                 .Select(cus => cus.ContactName)
                                                 .FirstOrDefault();
            }

            var orderDetails = await _context.OrderDetails
                              .Where(item => item.OrderId == id)
                              .ToListAsync();
            if (orders == null)
            {
                return NotFound();
            }
            else
            {
                Orders = orders;
                OrderDetails = orderDetails;
            }
            return Page();
        }
    }
}
