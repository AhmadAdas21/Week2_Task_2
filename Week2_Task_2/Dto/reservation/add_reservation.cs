using System.ComponentModel.DataAnnotations;
using Week2_Task_2.Dto.reservation_item;

namespace Week2_Task_2.Dto.reservation
{
    public class add_reservation
    {
        [Required]
        [MinLength(1)]
        public List<add_reservation_item> items { get; set; } = new();

        [Range(1, 10000)]
        public int customer_id { get; set; }
    }
}