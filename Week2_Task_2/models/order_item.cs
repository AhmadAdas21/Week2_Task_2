using System.ComponentModel.DataAnnotations;

namespace Week2_Task_2.models
{
    public class order_item
    {
        [Key]
        public int id { get; set; }
        public product product { get; set; }
        public int quantity { get; set; }
        public int prod_id { get; set; }
       // public customer customer { get; set; }
        public order order { get; set; }
        public order_item(int id,product p,int q,order c) {
            this.id = id;
            this.product = p;
            this.quantity = q;
            this.order = c;

        }
    }
}
