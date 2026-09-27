using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.customer;
using Week2_Task_2.services;

namespace Week2_Task_2.Controllers
{
    [ApiController]
    [Route("Api/customer")]


    public class customer : ControllerBase
    {
        private readonly data_base db;
        private readonly iservices _service;
        public customer(data_base data, iservices service)
        {

            db = data;
            _service = service;
        }

         [HttpGet]   
        public async Task< ActionResult<List<models.customer>>> GetAll()
        {
           var x= await db.Customers.ToListAsync();
            return Ok(x);   

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<data_base>>> Get_by_id(int id)
        {

            var x = await db.Customers.FirstOrDefaultAsync(x => x.id == id);
            if (x == null)
            {
                return NotFound();
            }
            return Ok(x);
        }
        [HttpPost]
       public async Task<ActionResult<response_customer>> Create(create_customer dto)
        {
            var customer = new models.customer
            {
                name = dto.name,
                email = dto.email
            };

            await _service.CreateAsync(customer);
        }
        [HttpPut]
        public async ActionResult<response_customer> update(update_customer dto, int id)
        {
            var x = await db.Customers.FirstOrDefaultAsync(x => x.id == id);
            if (x == null) {

                return NotFound();

            }
            x.name = dto.name;
            x.email = dto.email;
            
            await db.SaveChangesAsync();

            return NoContent();

        }

            [HttpDelete]
            public async ActionResult delete([FromRoute]int id) {

            }




        }
}
