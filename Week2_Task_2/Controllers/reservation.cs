using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Data;
using Week2_Task_2.Dto.reservation;
using Week2_Task_2.models;

namespace Week2_Task_2.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class reservation : ControllerBase
    {
        private readonly iservices_reservation service;
        private readonly ILogger<reservation> logger;
        private readonly data_base db;

        public reservation(iservices_reservation service, ILogger<reservation> logger, data_base db)
        {
            this.service = service;
            this.logger = logger;
            this.db = db;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] add_reservation dto)
        {
            var reservation = await service.Create(dto);
            logger.LogInformation("reservation with id{id}", reservation.id);
            await db.reservations.AddAsync(reservation);
            await db.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = reservation.id },
               new
               {
                   id = reservation.id,
                   customer_id = reservation.customer_id,
                   created_at = reservation.created_at,
                   expires_at = reservation.expires_at,
                   status = reservation.status
               }
           );
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var reservation = await service.GetById(id);
            if (reservation == null)
            {
                logger.LogWarning("the reservation with id {id}", id);
                return NotFound();
            }

            return Ok(reservation);
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (db.reservations.Count() == 0)
            {
                logger.LogWarning("theres no reservations yet");
                return NotFound();
            }
            return Ok(db.reservations);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> update(int id, [FromBody]
        {
            var res = await db.reservations.FirstOrDefaultAsync(x => x.id == id);
            if (res == null)
            {
                logger.LogWarning("the id {id}is not valid", id);
                return NotFound();
            }
            
        }
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var res = await db.reservations.FirstOrDefaultAsync(x => x.id == id);
            if (res == null)
            {
                logger.LogWarning("the id{id} is not va id", id);
                return NotFound();
            }
            await service.Cancel(id);

            return NoContent();
        }
    }
}
