namespace Week2_Task_2.models
{
    public class order_item
    {
        public int id { get; set; }
        public product product { get; set; }
        public int quantity { get; set; }
        
        public customer customer { get; set; }
        public order_item(int id,product p,int q,customer c) {
            this.id = id;
            this.product = p;
            this.quantity = q;
            this.customer = c;

        }
    }
}
