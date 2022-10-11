using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class CartProduct
    {
        [ForeignKey("product")]
        public int productId { get; set; }
        public Product product { get; set; }
        [ForeignKey("cart")]
        public int CartId { get; set; }
        public Cart cart { get; set; }
        public int NumberOfItems { get; set; }
    }
}
