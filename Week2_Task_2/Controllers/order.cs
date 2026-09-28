
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.customer;
using Week2_Task_2.Dto.orders;
using Week2_Task_2.services;

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

            se.add_order(dto);

            return Ok(dto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<response_order>>>update(int id,[FromBody]add_order dto)
        {
            var x=await dp.order.FirstOrDefaultAsync(x=>x.id==id);
            if (id < 0)
            {
                return BadRequest("the id must be above 0");
            }
            bool updated = se.update_customer(id, dto);
            if (updated)
            {
                return NoContent();
            }
            else
            {
                return NotFound("not founded");
            }
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
            se.remove_customer(x);
            return NoContent();


    }
}
