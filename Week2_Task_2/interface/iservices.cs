using Week2_Task_2.Controllers;

namespace Week2_Task_2
{
   public interface iservices
    {
        public void add_customer(customer c);
        public void remove_customer(customer customer);
        public void update_customer(customer cu);
        public void add_product(product product);
        public void remove_product(product product);
        public void update_product(product product);
        Task CreateAsync(models.customer customer);

        public void add_order
        
    }
}
