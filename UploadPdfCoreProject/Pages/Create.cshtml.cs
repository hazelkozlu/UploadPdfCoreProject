using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UploadPdfCoreProject.Models;

namespace UploadPdfCoreProject.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public Invoices Invoices { get; set; } 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Invoices.Add(Invoices);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
