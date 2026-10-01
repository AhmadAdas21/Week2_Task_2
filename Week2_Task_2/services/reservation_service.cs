using Week2_Task_2.models;
using Week2_Task_2.Data;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Week2_Task_2.services
{
    public class reservation_service : iservices_reservation
    {
        private readonly data_base db;
        private readonly ILogger<reservation_service> logger;

        public reservation_service(data_base db, ILogger<reservation_service> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public async Task<reservartion> Create(add_reservation dto)
        {
            var cus = await db.Customers.FirstOrDefaultAsync(x => x.id == dto.customer_id);
            if (cus == null)
            {
                logger.LogWarning("the customer with {id} not found", dto.customer_id);
                throw new Exception("customer does not exist");
            }
            if (dto.items == null)
            {
                logger.LogWarning("the items are null");
                throw new Exception("Reservation must contain at least one item");
            }
            var reservation = new reservartion
            {
                customer_id = dto.customer_id,
                customer = cus,
                created_at = DateTime.Now,
                expires_at = DateTime.Now.AddMinutes(15),
                status = "Active",
                items = new List<reservation_item>()
            };
            foreach (var i in dto.items)
            {
                var product = await db.prod.FirstOrDefaultAsync(p => p.id == i.product_id);
                if (product == null)
                {
                    logger.LogWarning("the product is null");
                    throw new Exception($"Product {i.product_id} does not exist");
                }
                if (product.active == false)
                {
                    logger.LogWarning("reservation creation failed, product {ProductId} is inactive", i.product_id);
                    throw new Exception("the product must be active");
                }
                if (i.quantity <= 0)
                {
                    logger.LogWarning("the quantity must be above 0");
                    throw new Exception("the quantity must be above 0");
                }
                if (i.quantity > product.stock)
                {
                    logger.LogWarning("the quantity must be below the product stock");
                    throw new Exception("the quantity is above the product stock");
                }
                product.stock -= i.quantity;
                logger.LogInformation("the quantity you need is reserved");
                reservation.items.Add(new reservation_item
                {
                    product_id = product.id,
                    product = product,
                    quantity = i.quantity
                });
            }

            await db.reservations.AddAsync(reservation);
            await db.SaveChangesAsync();
            return reservation;
        }
        public async Task <reservartion> GetById(int id)
        {
            var res = await db.reservations.FirstOrDefaultAsync(x => x.id == id);
            if (res == null)
            {
                logger.LogWarning("the reservation not existing");
                throw new Exception("the reservation dosent exist");
            }
            else
            {
                logger.LogInformation("the reservation exist");
                return res;
            }

        }
        public async Task<bool> Cancel(int id)
        {
            var res = await db.reservations.FirstOrDefaultAsync(x => x.id == id);
            if (res == null)
            {
                logger.LogWarning("the reservation id with id {id} dosent exist", id);
                throw new Exception("the reservation dosent exist");
            }
            foreach (var i in res.items)
            {
                i.product.stock += i.quantity;
            }
            res.status = "canceld";
            await db.SaveChangesAsync(); 
            return true;
        }

    }
}
