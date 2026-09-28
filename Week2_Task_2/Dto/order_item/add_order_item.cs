using System.ComponentModel.DataAnnotations;

namespace Week2_Task_2.Dto.order_item
{
    public class add_order_item
    {
        [Required]
        public int product_id { get; set; }

        [Required]
        public int quantity { get; set; }
    }
}
