using System.ComponentModel.DataAnnotations;

namespace ProcessRegistration.Models
{
    public class ProcessModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string NPU { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? ViewDate { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        public int CityCode { get; set; }
    }
}
