using System.ComponentModel.DataAnnotations;

namespace ActivityAcme.API.Resources
{
    public class SaveActivityResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}