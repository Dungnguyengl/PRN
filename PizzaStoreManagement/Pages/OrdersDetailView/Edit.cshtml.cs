using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.OrdersDetailView
{
    public class EditModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public EditModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetails OrderDetails { get; set; } = default!;
        public int OrderId { get; set; }
        public double ProductPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? productId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id && m.ProductId == productId);
            if (orderdetails == null)
            {
                return NotFound();
            }
            OrderDetails = orderdetails;
            OrderId = orderdetails.OrderId;
            var product = _context.Products.FirstOrDefault(p => p.ProductId == orderdetails.ProductId);
            if (product != null)
            {
                ProductPrice = product.UnitPrice;
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var product = _context.Products.FirstOrDefault(p => p.ProductId == OrderDetails.ProductId);
            if (product != null)
            {
                ProductPrice = product.UnitPrice;
            }
            OrderDetails.UnitPrice = ProductPrice;
            _context.Attach(OrderDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(OrderDetails.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = OrderDetails.OrderId });
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
