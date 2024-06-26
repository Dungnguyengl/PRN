using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.ProductsView
{
    public class DetailsModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public DetailsModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        public Products Products { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;
        public IList<Supplier> Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            Suppliers = await _context.Suppliers.ToListAsync();
            Categories = await _context.Categories.ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            else
            {
                Products = products;
            }
            return Page();
        }
    }
}
