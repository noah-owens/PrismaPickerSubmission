using System.ComponentModel.DataAnnotations;

namespace PrismaPicker.Models
{
    // Model Glass stores the important aspects of a Glass object, the fields used in the database for glass
    public class Glass
    {
        // Id
        public int Id { get; set; }

        // SKU
        [StringLength(15)]
        [Required]
        public string Sku { get; set; }

        // Name
        [StringLength(55)]
        [Required]
        public string Name { get; set; }

        // Color (validated against a regex for hexidecimal color codes)
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        [Required]
        public string Color { get; set; }

        // Link value
        [StringLength(55)]
        [Required]
        public string Link { get; set; }
    }
}
