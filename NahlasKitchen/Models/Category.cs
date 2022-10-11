using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class Category
    {
        public Category()
        {
            products = new HashSet<Product>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(50 , ErrorMessage ="Maximum 50 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Maximum 250 characters")]
        public string Description { get; set; }
        public virtual ICollection<Product> products { get; set; }

    }
}
