using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAnimalWorld.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Continent))]
        public int ContinentId { get; set; }

        public Continent? Continent { get; set; }
    }
}
