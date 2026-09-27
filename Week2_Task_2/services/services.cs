using Microsoft.EntityFrameworkCore;

namespace Week2_Task_2.services
{
    public class services:iservices
    {

        public async Task<customer> AddCustomer(customer customer)
        {
            await data_base.Customers.AddAsync(customer);

            await data_base.SaveChangesAsync();

            return customer;
        }

    }
}
