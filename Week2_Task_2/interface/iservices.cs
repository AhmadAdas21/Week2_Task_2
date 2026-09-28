using Week2_Task_2.Controllers;
using Week2_Task_2.Dto.customer;

namespace Week2_Task_2
{
   public interface iservices
    {
        public void add_customer(customer c);
        public void remove_customer(customer customer);
        public void update_customer(int id, customer cu);
        public void add_product(product product);
        public void remove_product(product product);
        public void update_product(product product);
        Task CreateAsync(models.customer customer);
        void add_product(models.product x);
        object? getall();
        string? add_order(Dto.orders.add_order dto);
        bool update_customer(int id, update_customer dto);
        void remove_customer(models.order? x);
        void remove_customer(models.customer x);

        public void add_order
        
    }
}
