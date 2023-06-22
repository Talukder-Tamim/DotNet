using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CustomerCreate
    {
        [Required(ErrorMessage = "Please type your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please type your Phone")]
        public string Phone { get; set; }
        public string Email { get; set; }   
        
    }
}
