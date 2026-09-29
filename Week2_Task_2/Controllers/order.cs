
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.customer;
using Week2_Task_2.Dto.orders;
using Week2_Task_2.services;
using Week2_Task_2.models;

namespace Week2_Task_2.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class order : ControllerBase
    {
        private readonly data_base dp;
        private readonly iservices se;

        public order(data_base dp, iservices se)
        {
            this.dp = dp;
            this.se = se;
        }

        [HttpGet]
        public async Task<ActionResult<List<order>>> GetAll()
        {
          
            var x = await dp.order.ToListAsync();
            return Ok(x); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<order>>>get_by_id(int id)
        {
            var x = await dp.order.FirstOrDefaultAsync(x => x.id == id);

            if (x == null)
            {
                return NotFound();
            }

            return Ok(x);
        }
        [HttpPost]
        public async Task<ActionResult<response_order>> Create([FromBody] add_order dto)
        {
            if (dto == null)
            {
                return BadRequest("the form is null");
            }

            if (dto.customer_id < 0 || await dp.Customers.FirstOrDefaultAsync(x => x.id == dto.customer_id) == null)
            {
                return BadRequest("the customer id is invalid or the customer does not exist");
            }

            var x = await dp.Customers.FirstOrDefaultAsync(x => x.id == dto.customer_id);
            foreach (var o in dto.items)
            {
                var p= await dp.prod.FirstOrDefaultAsync(p => p.id == o.product_id);

                if (p == null)
                {
                    return BadRequest( "product  does not exist" );
                }
            }

            var s = await dp.order.FirstOrDefaultAsync(x => x.customer_id == dto.customer_id);

           
            var d = await dp.oi.FirstOrDefaultAsync(x => x.order_id == s.id);
            var prod = await dp.prod.FirstOrDefaultAsync(x => x.id == d.product_id);
            if (d == null || d.product == null) 
            {
                return BadRequest("the product is null");
            }
            if (d.product.active == false)
            {
                return BadRequest("the product is not active");
            }
            if (d.quantity <= 0)
            {
                return BadRequest("the quantity must be grater than 0");
            }
            if (d.quantity > prod.stock)
            {
                return BadRequest("the product quantity in the order is more than the product in the stock");
            }
            if (s.order_items == null)
            {
                return BadRequest("the order must have one item ");
            }


            var order = new models.order
            {
                customer = x,
                customer_id = dto.customer_id,
                status = "not completed",
                created_date = DateTime.Now,
                order_items = new List<models.order_item>()
            };
            

            await dp.order.AddAsync(order);
            await dp.SaveChangesAsync();
            prod.stock-=d.quantity;

            return CreatedAtAction(nameof(get_by_id), new { id = order.id }, order);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<response_order>>>update(int id,[FromBody]add_order dto)
        {
            var x=await dp.order.FirstOrDefaultAsync(x=>x.id==id);
            if (id < 0)
            {
                return BadRequest("the id must be above 0");
            }
            if (x.status == "complete")
            {
                return BadRequest("you cant modify a complete order");
            }
            
            x.customer_id=dto.customer_id;
           
          //x.status = dto.status;


            return NoContent();
            //validate(dto);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<response_order>>> delete(int id)
        {
            var x = await dp.order.FirstOrDefaultAsync(x => x.id == id);
            if (id < 0)
            {
                return BadRequest("must be above 0");
            }

            if (x == null)
            {
                return NotFound("Order not found");
            }

            dp.order.Remove(x); 
            await dp.SaveChangesAsync(); 

            return NoContent();
        }

    }
}
