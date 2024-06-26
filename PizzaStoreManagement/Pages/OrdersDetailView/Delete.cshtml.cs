using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.OrdersDetailView
{
    public class DeleteModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public DeleteModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetails OrderDetails { get; set; } = default!;
        public int? OrderId { get; set; }    

        public async Task<IActionResult> OnGetAsync(int? id, int? productId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderdetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id && m.ProductId == productId);

            OrderId = orderdetails?.OrderId;
            if (orderdetails == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetails = orderdetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? productId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id && m.ProductId == productId);
            if (orderdetails != null)
            {
                OrderDetails = orderdetails;
                _context.OrderDetails.Remove(OrderDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
