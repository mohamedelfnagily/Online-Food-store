using Microsoft.EntityFrameworkCore;
using NahlasKitchen.Data;
using NahlasKitchen.Models;


namespace NahlasKitchen.EntityManager.ManageProduct
{
    public class ManageProduct : IManageProduct
    {
        AppdbContext db = new AppdbContext();
        public void AddProduct(Product prod, IFormFile Image1, IFormFile Image2 , IFormFile Image3)
        {
            //Generating product Id
            var allProducts = db.Products.ToList();
            int id = 1;
            if(allProducts.Count > 0)
            {
                id = allProducts.Max(e => e.Id) + 1;
            }
            prod.Id = id;
            //Setting Products Image
            if(Image1!=null)
            {
                AddProductImage(Image1, 1, prod);
            }
            if(Image2!=null)
            {
                AddProductImage(Image2, 2, prod);
            }
            if(Image3!=null)
            {
                AddProductImage(Image3, 3, prod);
            }
            prod.Rating = 5;
            db.Products.Add(prod);
            db.SaveChanges();


        }

        public void AddProductImage(IFormFile picture, int index , Product p)
        {
            if (picture.FileName != null)
            {
                string imageextension = picture.FileName.Split('.')[1].ToString();
                string ImageName = p.Name.Replace(' ','-')+ p.Id + "-" + index+"."+imageextension;
                if(index==1)
                {
                    p.Image1 = ImageName;
                }
                else if(index==2)
                {
                    p.Image2 = ImageName;
                }
                else
                {
                    p.Image3 = ImageName;
                }
                using (var obj = new FileStream(@".\wwwroot\KitchenImages\" + ImageName, FileMode.Create)) 
                {
                    picture.CopyTo(obj);
                }
                
            }
        }

        public void changePicture(IFormFile picture, int index)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            var myProd = db.Products.FirstOrDefault(e => e.Id == id);
            if (myProd!=null)
            {
                db.Products.Remove(myProd);
                db.SaveChanges();
            }

        }

        public void EditProduct(Product prod)
        {
            int id = prod.Id;
            var myProd = GetProductById(id);
            myProd.Name = prod.Name;
            myProd.Description = prod.Description;
            myProd.Price = prod.Price;
            myProd.OnSale = prod.OnSale;
            myProd.Rating = prod.Rating;
            myProd.CategoryId = prod.CategoryId;
            db.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> myPRods = db.Products.Include(e => e.Category).ToList();
            return myPRods;
        }

        public Product GetProductById(int id)
        {
            Product myProd = GetAllProducts().FirstOrDefault(e => e.Id == id);
            return myProd;
        }
    }
}
