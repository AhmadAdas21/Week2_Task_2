
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.customer;
using Week2_Task_2.Dto.orders;
using Week2_Task_2.services;

namespace Week2_Task_2.Controllers
{
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
        [HttpGet("{id")]
        public async Task<ActionResult<List<order>>>get_by_id(int id)
        {
            var x=await dp.order.FirstAsync(x=>x.id==id);
            return Ok(x);
        }
        [HttpPost]
        public async ActionResult<response_order> Create([FromBody]create_customer dto)
        {
          //  validate(dto);
            var c=se.add_customer(dto);
            return CreatedAtAction(c);

         //  ActionResult
        }
        [HttpPut]
        public async ActionResult<response_order> update([FromBody]update_customer dto,int id)
        {
            var x=await dp.order.FirstOrDefaultAsync(x=>x.id==id);
            if (id < 0)
            {
                return BadRequest("the id must be above 0")
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
            //validate(dto);

        }
        [HttpDelete("{id}")]
        public async ActionResult<response_order> delete(int id)
        {
            var x = await dp.order.FirstOrDefaultAsync(x => x.id == id);
            if (id < 0) {
                return BadRequest("must be above 0")}
            se.remove_customer(x);


    }
}
