using Microsoft.EntityFrameworkCore;
using NahlasKitchen.Data;
using NahlasKitchen.Models;

namespace NahlasKitchen.EntityManager.ManageCart
{
    public class ManageCart : IManageCart
    {
        AppdbContext db = new AppdbContext();

        public void AddToCart(int PId , int cartId)
        {
            var myCart = db.Carts.FirstOrDefault(e => e.Id == cartId);
            CartProduct myPC = new CartProduct { CartId = cartId, productId = PId };
            var myProdCarts = db.CartProduct.FirstOrDefault(e=>e.productId==PId&&e.CartId==cartId);
            if(myProdCarts !=null)
            {
                myProdCarts.NumberOfItems += 1;
                db.SaveChanges();
            }
            else
            {
                myPC.NumberOfItems = 1;
                db.CartProduct.Add(myPC);
                db.SaveChanges();
            }
            


        }

        public void AssignCartToUser(int userid)
        {
            Cart cart = new Cart();
            var Carts = db.Carts.ToList();
            int id = 1;
            if(Carts.Count>0)
            {
                id = Carts.Max(e => e.Id) + 1;
            }
            cart.Id = id;
            cart.userId = userid;
            db.Carts.Add(cart);
            db.SaveChanges();
        }

        public void ClearCart(int CartId)
        {
            var ProductsInCart = db.CartProduct.Where(e => e.CartId == CartId).ToList();
            if(ProductsInCart.Count>0)
            {
                foreach(var prod in ProductsInCart)
                {
                    db.CartProduct.Remove(prod);
                    
                }
                var myCart = db.Carts.FirstOrDefault(e => e.Id == CartId);
                myCart.TotalPrice = 0;
                myCart.Size = 0;
                db.SaveChanges();
            }
        }

        public void clearCart(int userId)
        {
            var Cart = db.Carts.FirstOrDefault(e => e.userId == userId);
            var ProductsInCart = db.CartProduct.Where(e => e.CartId == Cart.Id).ToList();
            foreach(var item in ProductsInCart)
            {
                db.CartProduct.Remove(item);
            }
            db.SaveChanges();

        }

        public Cart getCartById(int id)
        {
            var myCarts = db.Carts.ToList();
            if(myCarts.Count>0)
            {
               var myCart =  myCarts.FirstOrDefault(e => e.Id == id);
                return myCart;
            }
            else
            {
                return null;
            }
        }

        public int getTotalPrice(int userId)
        {
            var myPRoducts = getUserProducts(userId);
            int sum = 0;
            if(myPRoducts.Count>0)
            {
                foreach (var product in myPRoducts)
                {
                    sum += product.Price;
                }
            }
            return sum;
        }

        public List<Product> getUserProducts(int userId)
        {
            var Cart = db.Carts.FirstOrDefault(e => e.userId == userId);
            var ProductsInCart = db.CartProduct.Where(e => e.CartId == Cart.Id).ToList();
            var myPRoducts = db.Products.Include(e => e.Category).ToList();

            if (Cart!=null && ProductsInCart!=null)
            {
                List<Product> myProductList = new List<Product>();
                foreach(var item in ProductsInCart)
                {
                    if(item.NumberOfItems==1)
                    {
                        myProductList.Add(myPRoducts.FirstOrDefault(e=>e.Id==item.productId));
                    }
                    else
                    {
                        for(int i=0;i<item.NumberOfItems;i++)
                        {
                            int prodId = item.productId;
                            myProductList.Add(myPRoducts.FirstOrDefault(e => e.Id == prodId));
                        }
                    }
                }
                return myProductList;
            }
            else
            {
                return null;
            }
        }

        public void RemoveFromCart(Product P)
        {
            var productId = P.Id;
            var productCart = db.CartProduct.FirstOrDefault(e => e.productId == productId);
            if(productCart.NumberOfItems==1)
            {
                db.CartProduct.Remove(productCart);
            }
            else
            {
                productCart.NumberOfItems -= 1;
            }
            
            db.SaveChanges();
        }
    }
}
