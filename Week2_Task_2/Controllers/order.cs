
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
