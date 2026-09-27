using System.ComponentModel.DataAnnotations;
namespace Week2_Task_2.models
{
    public class product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }

        public product (int id, string name, string description, float price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
        }
    }
}
