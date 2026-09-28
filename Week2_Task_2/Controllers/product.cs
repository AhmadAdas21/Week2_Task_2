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
            var c = await _data.prod.ToListAsync();
            return Ok(c);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<response_prod>> get_by_id(int d)
        {
            if (d < 0)
            {
                return BadRequest("the Is must be greater than 0or zero");
            }
            var x = await _data.prod.FirstOrDefaultAsync(x => x.id == d);

            if (x == null)
            {
                return BadRequest("invalid id num");
            }
            return Ok(x);
        }

        [HttpPost]
        public async Task<ActionResult<response_prod>> Create([FromBody] add_prod d)
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
            await _data.prod.AddAsync(x);
            await _data.SaveChangesAsync();

           
            return CreatedAtAction(nameof(get_by_id), new { id = x.id }, x);
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
            x.active = d.active;
            x.stock = d.stock;

            await _data.SaveChangesAsync();


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
            _data.prod.Remove(x);
            await _data.SaveChangesAsync();
        }
    }
}
