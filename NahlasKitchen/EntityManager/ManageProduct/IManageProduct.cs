using NahlasKitchen.Models;

namespace NahlasKitchen.EntityManager.ManageProduct
{
    public interface IManageProduct
    {
        //Getting all products
        public List<Product> GetAllProducts();
        //Getting a product by id
        public Product GetProductById(int id);
        //Adding new product
        public void AddProduct(Product prod, IFormFile Image1, IFormFile Image2, IFormFile Image3);
        //Removing product
        public void DeleteProduct(int id);
        //Editing a product
        public void EditProduct(Product prod);
        //Changing Product Image
        //we have 3 indexes for the images
        //Index = 1 represents the first image
        public void changePicture(IFormFile picture , int index);

        //Add product picture
        public void AddProductImage(IFormFile picture, int index, Product p);
    }
}
