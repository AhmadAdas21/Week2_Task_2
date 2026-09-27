namespace Week2_Task_2.models
{
    public class order
    {
        public int id { get; set; }
        public string name { get; set; }
        public   order_item item { get; set; }
        public float total { get; set; }
        public customer customer { get; set; }

        public order(int id,string name,order_item item,float total,customer customer)
        {
            this.id = id;
            this.name = name;
            this.item = item;
            this.total = total;
            this.customer = customer;
        }
        

    }
}
