using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadPdfCoreProject.Models;

namespace UploadPdfCoreProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public List<Invoices> Invoices { get; set; }

        public void OnGet()
        {
            Invoices=_context.Invoices.ToList();

        }

        public async Task<IActionResult> OnPostDownloadAsync(int? id)
        {
            var myInvoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (myInvoice == null)
            {
                return NotFound();  
            }
            if (myInvoice.Attachment==null)
            {
                return Page();

            }
            else
            {
                byte[] byteArr = myInvoice.Attachment;
                string mimeType = "application/pdf";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"Invoice Number {myInvoice.Number}.pdf"
                };
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            var myInvoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (myInvoice == null)
            {
                return NotFound();
            }
            if (myInvoice.Attachment == null)
            {
                return Page();

            }
            else
            {
                myInvoice.Attachment = null;
                _context.Update(myInvoice);
                await _context.SaveChangesAsync();  
            }
            Invoices = await _context.Invoices.ToListAsync();
            return Page();
        }
    }
}
