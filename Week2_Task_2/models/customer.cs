using System.ComponentModel.DataAnnotations;
namespace Week2_Task_2.models
{
    public class customer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }



        public customer(int id,string name,string email)
        {
            this.id = id;
            this.name = name;
            this.email = email;
        }

    }
}
