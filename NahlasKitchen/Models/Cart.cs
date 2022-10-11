using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class Cart
    {
        public Cart()
        {
            cartProduct=new HashSet<CartProduct>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int Size { get; set; }
        public int TotalPrice { get; set; }
        public virtual ICollection<Product> products { get; set; }
        [ForeignKey("user")]
        public int userId { get; set; }
        public virtual User user { get; set; }
        public virtual ICollection<CartProduct> cartProduct { get; set; }

    }
}
