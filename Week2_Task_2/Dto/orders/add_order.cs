using System.ComponentModel.DataAnnotations;
using Week2_Task_2.models;

namespace Week2_Task_2.Dto.orders
{
    public class add_order
    {
        //  public string name { get; set; }
        //  order s = new order(int id ,);
        [Required]
        public string name { get; set; }
        [Required]
        public float total { get; set; }
        [Required]
        public int customer_id { get; set; }
    }
}
