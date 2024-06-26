using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;
using static NuGet.Packaging.PackagingConstants;

namespace PizzaStoreManagement.Pages.OrdersView
{
    public class EditModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public EditModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Orders Orders { get; set; } = default!;
        public string? CustomerName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }
            Orders = orders;
            CustomerName = await _context.Customers.Where(m => m.CustomerId == orders.CustomerId)
                .Select(c => c.ContactName)
                .FirstOrDefaultAsync();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "ContactName");
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
            _context.Attach(Orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(Orders.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
