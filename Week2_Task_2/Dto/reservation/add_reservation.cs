using System.ComponentModel.DataAnnotations;
using Week2_Task_2.models;

namespace Week2_Task_2.Dto.reservation
{
    public class add_reservation
    {
      
     // [Required]

   //  public int prod_id { get; set; }
        [Required]
        public List<reservation_item> items { get; set; }

        [Required]
        public int customer_id { get; set; }
    }
}
