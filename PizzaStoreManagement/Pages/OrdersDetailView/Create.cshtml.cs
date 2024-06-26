using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.OrdersDetailView
{
    public class CreateModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public CreateModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }
        public int Id { get; set; }
        public Dictionary<int, double> ProductPrices;
        public IActionResult OnGet(int id)
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            Id = id;
            // Prepare product prices to be used in JavaScript
            var productPrices = new Dictionary<int, double>();
            foreach (var product in _context.Products)
            {
                productPrices.Add(product.ProductId, product.UnitPrice);
            }
            ProductPrices = productPrices;
            return Page();
        }

        [BindProperty]
        public OrderDetails OrderDetails { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var orderDetails = await _context.OrderDetails
                .Where(o => o.OrderId == OrderDetails.OrderId && o.ProductId == OrderDetails.ProductId)
                .FirstOrDefaultAsync();
            if (orderDetails == null) {
                _context.OrderDetails.Add(OrderDetails);
            }
            else
            {
                orderDetails.Quantity += OrderDetails.Quantity;
                _context.Attach(orderDetails).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = OrderDetails.OrderId });
        }

        public async Task<JsonResult> OnGetGetProductPriceAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return new JsonResult(new { price = 0 });
            }
            return new JsonResult(new { price = product.UnitPrice });
        }
    }
}
