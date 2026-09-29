using Week2_Task_2.Dto.customer;
using Week2_Task_2.models;


namespace Week2_Task_2
{
    public interface iservices
    {
      //Task<List<customer>> get_all();

        Task<customer?> get_by_id(int id);

        Task<customer> Create(customer customer);

        Task get_products(int page, object pageSize, string? search, object minPrice, object maxPrice, bool inStock, object sortBy, object sortDirection);

        Task<bool> Delete(int id);
      //Task get_products(int page, object pageSize, string? search, object minPrice, object maxPrice, bool inStock, object sortBy, object sortDirection);
        Task get_products(int page, int size, string? search, int min, int max, bool inStock, string sortby);
    }
}