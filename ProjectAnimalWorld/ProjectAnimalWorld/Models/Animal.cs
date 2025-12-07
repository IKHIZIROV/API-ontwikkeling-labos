using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnimalWorld.Models
{
    [Table("Animals")]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(80)]
        public string Species { get; set; } = string.Empty;

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country? Country { get; set; }
    }
}
