using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStoreManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PizzaStoreManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStoreManagement.Models.PizzaStoreDBContext _context;

        public IndexModel(PizzaStoreManagement.Models.PizzaStoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                var account = await _context.Account
                    .FirstOrDefaultAsync(a => a.UserName == Input.UserName && a.Password == Input.Password);

                if (account != null)
                {
                    // Set authentication cookie or session
                    HttpContext.Session.SetString("UserName", account.UserName);
                    HttpContext.Session.SetInt32("AccountType", (int)account.Type);

                    return RedirectToPage("/Privacy");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
