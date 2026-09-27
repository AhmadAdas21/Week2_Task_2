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
        await _context.Customers.ToListAsync();





    }
}
