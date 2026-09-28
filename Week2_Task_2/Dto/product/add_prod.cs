using System.ComponentModel.DataAnnotations;

namespace Week2_Task_2.Dto.product
{
    public class add_prod
    {
        [Required]
        public string name { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string ksu { get; set; }

        [Required]
        public int stock { get; set; }
        [Required]
        public bool active { get; set; }

    }
}
