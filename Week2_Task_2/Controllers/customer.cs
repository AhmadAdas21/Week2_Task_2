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
    [ApiController ]
    [Route("Api/customer")]

    
    public class customer:ControllerBase
    {
        private readonly data_base db;
        private readonly iservices _service;
        public customer(data_base data)
        {
            db = data;
        }

        public customer(iservices service)
        {
            _service = service;
        }
        [HttpGet]   
        public async Task< ActionResult<List<data_base>>> GetAll()
        {
           var x= await db.Customers.ToListAsync();
            return Ok(x);   

        }
        [HttpGet]
        public async Task<ActionResult<List<data_base>>> Get_by_id(int id)
        {
            var x = db.Customers.FirstOrDefaultAsync(x => x.id == id);
            return Ok(x);
        }
        [HttpPost]
        public async ActionResult<response_customer >Create([FromBody]create_customer dto)
        {
            try
            {
              await  _service.add_customer(dto);
            }
            catch (Exception ex) {
            {
                    new 

            }
        }
        [HttpPut]
        public async ActionResult<response_customer>update(update_customer dto,int id)
            {
                if(db.Customers.Any(x => x.id == id))
                {
                    return;
                }
                else
                {
                    throw new Exception("the user not found");
                }

            }

            [HttpDelete]
            public async ActionResult delete([FromRoute]int id) {

            }




        }
}
