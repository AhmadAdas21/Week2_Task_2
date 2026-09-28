using System;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.models;
using Week2_Task_2.Data;



namespace Week2_Task_2.services
{
    public class services : iservices
    {

      /*  public async Task<customer> AddCustomer(customer customer)
        {
            await data_base.Customers.AddAsync(customer);

            await data_base.SaveChangesAsync();

            return customer;
        }*/
        private readonly data_base _data;

        public services(data_base c)
        {
            _data = c;
        }
      

        public async Task<List<customer>> GetAllAsync()
        {
            return await _data.Customers.ToListAsync();
        }

        public async Task<customer?> GetByIdAsync(int id)
        {
            return await _data.Customers
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task<customer> CreateAsync(customer customer)
        {
            await _data.Customers.AddAsync(customer);

            await _data.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _data.Customers.FindAsync(id);

            if (customer == null)
                return false;

            _data.Customers.Remove(customer);

            await _data.SaveChangesAsync();

            return true;
        }

       



    }
}
