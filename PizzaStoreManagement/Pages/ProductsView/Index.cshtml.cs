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
    public class IndexModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public IndexModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }
        public IList<Products> Products { get; set; }
        public IList<Supplier> Suppliers { get; set; }
        public IList<Category> Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPrice { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Products> productQuery = _context.Products;

            if (!string.IsNullOrEmpty(SearchId) && int.TryParse(SearchId, out int id))
            {
                productQuery = productQuery.Where(p => p.ProductId == id);
            }

            if (!string.IsNullOrEmpty(SearchName))
            {
                productQuery = productQuery.Where(p => p.ProductName.Contains(SearchName));
            }

            if (!string.IsNullOrEmpty(SearchPrice) && double.TryParse(SearchPrice, out double price))
            {
                productQuery = productQuery.Where(p => p.UnitPrice == price);
            }

            Products = await productQuery.ToListAsync();
            Suppliers = await _context.Suppliers.ToListAsync();
            Categories = await _context.Categories.ToListAsync();
        }

    }
}
