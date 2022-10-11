using NahlasKitchen.Models;

namespace NahlasKitchen.EntityManager.ManageCart
{
    public interface IManageCart
    {
        //Add product to the cart
        public void AddToCart(int PId, int cartId);

        //Remove product from cart
        public void RemoveFromCart(Product P);

        //Clear the cart
        public void ClearCart(int CartId);

        //Assign Cart To user
        public void AssignCartToUser(int userId);
        //Get user's Cart products
        public List<Product> getUserProducts(int userId);
        //Getthing the cart by Id
        public Cart getCartById(int id);
        //Get the total price of items in cart
        public int getTotalPrice(int userId);
        //Removing all items from cart
        public void clearCart(int userId);

        
        
    }
}
