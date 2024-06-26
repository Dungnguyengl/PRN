using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;

namespace PizzaStoreManagement.Pages.AccountsView
{
    public class EditModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;
        public SelectList AccountTypes { get; set; }
        public EditModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
            AccountTypes = new SelectList(Enum.GetValues(typeof(AccountType)));
        }

        [BindProperty]
        public Accounts Accounts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts =  await _context.Account.FirstOrDefaultAsync(m => m.AccountId == id);
            if (accounts == null)
            {
                return NotFound();
            }
            Accounts = accounts;
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

            _context.Attach(Accounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(Accounts.AccountId))
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

        private bool AccountsExists(int id)
        {
            return _context.Account.Any(e => e.AccountId == id);
        }
    }
}
