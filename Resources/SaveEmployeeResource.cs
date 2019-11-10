using System.ComponentModel.DataAnnotations;

namespace ActivityAcme.API.Resources
{
    public class SaveEmployeeResource
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Comments { get; set; }

        [Required]
        public int ActivityId { get; set; }
    }
}