using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   
        public int Id { get; set; }
        public string IsDelivered { get; set; }
        public int OrderTotal { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        [RegularExpression("^[0125][0-9]{8}$", ErrorMessage = "Egyptian numbers only")]
        public int MobileNumber { get; set; }
        [ForeignKey("Cart")]
        public int cartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
