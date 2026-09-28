using Week2_Task_2.Controllers;
using Week2_Task_2.Dto.customer;

namespace Week2_Task_2
{
    public interface iservices
    {
        Task<List<customer>> get_all();

        Task<customer?> get_by_id(int id);

        Task<customer> Create(customer customer);

        Task<bool> Delete(int id);
    }
}