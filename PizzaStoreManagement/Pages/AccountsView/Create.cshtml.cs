using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.AccountsView
{
    public class CreateModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;
        public SelectList AccountTypes { get; set; }
        public CreateModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            AccountTypes = new SelectList(Enum.GetValues(typeof(AccountType)));
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Accounts Accounts { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AccountTypes = new SelectList(Enum.GetValues(typeof(AccountType)));
                return Page();
            }

            _context.Account.Add(Accounts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
