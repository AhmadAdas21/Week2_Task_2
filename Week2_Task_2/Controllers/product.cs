using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.customer;
using Week2_Task_2.Dto.product;
using Week2_Task_2.services;
using Week2_Task_2.models;

namespace Week2_Task_2.Controllers
{
    [ApiController]
    [Route("API/product")]
    public class product : ControllerBase
    {
        private readonly data_base _data;
        private readonly iservices s;

        public product(iservices service, data_base d)
        {
            _data = d;
            s = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<response_prod>>> getall()
        {
            return Ok(s.getall());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<response_prod>> get_by_id(int d)
        {
            var x = await _data.prod.FirstOrDefaultAsync(x => x.id == d);

            if (x == null)
            {
                return BadRequest("invalid id num");
            }
            return Ok(x);
        }

        [HttpPost]
        public async Task<ActionResult<response_prod>> crate([FromBody] add_prod d)
        {
            if (d == null)
            {
                return BadRequest("the form is null");
            }

            var x = new models.product
            {
                name = d.name,
                price = d.price,
                description = d.description,
                ksu = d.ksu
            };

            s.add_product(x);

            return Ok("Product created successfully");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<response_prod>>update(update_prod d,int id)
        {
            if (id < 0)
            {
                return BadRequest("the id must be grater than 0");
            }
            var x = await _data.prod.FirstOrDefaultAsync(x => x.id == id);
            if (x == null)
            {
                return BadRequest("the product is null");
            }
            x.name = d.name;
            x.price = d.price;
            x.description = d.description;
            return NoContent();
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<response_prod>>delete(int id)
        {
            if(id < 0)
            {
                return BadRequest("must be above 0");

            }
            var x = await _data.prod.FirstOrDefaultAsync(x => x.id == id);
            return Ok(s.remove_product);
        }
    }
}
