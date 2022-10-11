using NahlasKitchen.Models;

namespace NahlasKitchen.Models.ViewModels
{
    public class CartViewModel
    {
        public List<Product> prods { get; set; }
        public int TotalPrice { get; set; }
    }
}
