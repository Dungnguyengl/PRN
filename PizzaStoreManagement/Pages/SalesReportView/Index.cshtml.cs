using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages
{
    public class SalesReportModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public SalesReportModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }


        [BindProperty(SupportsGet = true)]
        public string StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string EndDate { get; set; }

        public List<SalesViewModel> Sales { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Orders> orderQuery = _context.Orders;

            DateTime startDate;
            DateTime endDate;

            if (DateTime.TryParse(StartDate, out startDate))
            {
                orderQuery = orderQuery.Where(o => o.OrderDate.Day >= startDate.Day);
            }

            if (DateTime.TryParse(EndDate, out endDate))
            {
                orderQuery = orderQuery.Where(o => o.OrderDate.Day <= endDate.Day);
            }

            Sales = await orderQuery
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new SalesViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.Frieght // Assuming this is the total amount for simplicity
                })
                .ToListAsync();

            return Page();
        }

        public class SalesViewModel
        {
            public int OrderId { get; set; }
            public DateOnly OrderDate { get; set; }
            public double TotalAmount { get; set; }
        }
    }
}

