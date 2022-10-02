using Microsoft.EntityFrameworkCore;

namespace UploadPdfCoreProject.Models
{
    public partial class ApplicationDbContext:DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-2ID4KL6;Database=InvoiceDB;Integrated Security=true");
        //}

        public virtual DbSet<Invoices> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoices>(entity => {
                entity.Property(x => x.Date).HasColumnType("datetime");
                entity.Property(x => x.Value).HasColumnType("decimal(18,2)");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
