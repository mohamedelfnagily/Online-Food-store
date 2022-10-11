using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class Product
    {
        public Product()
        {
            cartProduct = new HashSet<CartProduct>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum length 20 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Maximum length 20 characters")]
        public string Description { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual ICollection<CartProduct> cartProduct { get; set; }
        public string OnSale { get; set; }
        [Range(1,5)]
        public int? Rating { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
