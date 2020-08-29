using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        //then create an internal list of product
        List<Product> products = new List<Product>();
        //create constructor to create inital standardization
        public ProductRepository()

        {
            products = cache["products"] as List<Product>;
            // if no "products" list cache  exists create one
            if (products == null) {
                products = new List<Product>();
            }
        }
        //then create a method called commit
        public void Commit()
        {
            cache["products"] = products;
        }
        //then create the list functionality like insert delete edit 
        public void Insert(Product p) {
            products.Add(p);
        }

        public void Update(Product product) {
            //looks for the product you want to update 
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            
            //if there is somehting there it will automatically update the list 
            if (productToUpdate != null) {
                productToUpdate = product;
            }
            else {
                throw new Exception("Product Not Found!");
            }

        }

        //return a single product 
        public Product Find(string Id) {
            Product product = products.Find(p => p.Id == Id);

            //if there is somehting there it will automatically update the list 
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }
        //returns a list that can ebe queried
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable(); 

        }
        public void Delete(string Id) {
            Product productToDelete = products.Find(p => p.Id == Id);

            //if there is somehting there it will automatically update the list 
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }
    }

}
