using Microsoft.AspNetCore.Mvc;
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

        public reservation(iservices_reservation service, ILogger<reservation> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] add_reservation dto)
        {
            var reservation = await service.Create(dto);
            logger.LogInformation("reservation with id{id}", reservation.id);
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
    }
}
