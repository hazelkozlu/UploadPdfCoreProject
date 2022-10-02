using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadPdfCoreProject.Models;

namespace UploadPdfCoreProject.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public UploadModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int? myID { get; set; }
        [BindProperty]
        public IFormFile  file { get; set; }
        [BindProperty]
        public int? ID { get; set; }
        public void OnGet(int? id)
        {
            myID = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (file!= null)
            {
                if (file.Length > 0 && file.Length>300000)
                {
                    var myInvoice = _context.Invoices.FirstOrDefault(x=>x.Id==ID);
                    using (var target= new MemoryStream())
                    {
                        file.CopyTo(target); 
                        myInvoice.Attachment=target.ToArray();  
                    }
                    _context.Invoices.Update(myInvoice);  
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
