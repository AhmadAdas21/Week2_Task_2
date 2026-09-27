using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Controllers;
using Week2_Task_2.models;
namespace Week2_Task_2.Data
{
    public class data_base : DbContext
    {


        public data_base(DbContextOptions<data_base> options):base(options)
        {



        }
        public DbSet<models.customer> Customers { get; set; }
        public DbSet<models.order> order { get; set; }
        public DbSet<models.product> prod { get; set; }
        public DbSet<models.order_item> oi { get; set; }
        // await _context.Customers.ToListAsync();
        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<models.customer>()
                .HasIndex(c => c.id)
                .IsUnique();


            modelBuilder.Entity<models.product>()
                .Property(p => p.price)
                .HasPrecision(18, 2);


            modelBuilder.Entity<models.order>()
                .HasOne(o => o.customer)
                .WithMany(c => c.order)
                .HasForeignKey(o => o.CustomerId);


            modelBuilder.Entity<models.order_item>()
                .HasOne(oi => oi.order)
                .WithMany(o => o.)
                .HasForeignKey(oi => oi.id);


            modelBuilder.Entity<models.order_item>()
                .HasOne(oi => oi.product)
                .WithMany(p => p.order)
                .HasForeignKey(oi => oi.ProductId);
        }





    }
}
