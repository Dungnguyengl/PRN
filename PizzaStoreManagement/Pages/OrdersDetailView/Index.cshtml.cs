using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.OrdersDetailView
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public IndexModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        public IList<OrderDetails> OrderDetails { get;set; } = default!;
        public int? OrderId { get; set; }    

        public async Task OnGetAsync(int? id)
        {
            OrderDetails = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Products)
                .Where(od => od.OrderId == id)
                .ToListAsync();
            OrderId = id;
        }
    }
}
