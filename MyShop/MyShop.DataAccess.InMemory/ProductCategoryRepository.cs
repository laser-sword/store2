using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        //then create an internal list of product
        
        
        List<ProductCategory> productCategories;
        //create constructor to create inital standardization
        public ProductCategoryRepository()

        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            // if no "products" list cache  exists create one
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        //then create a method called commit
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        //then create the list functionality like insert delete edit 
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            //looks for the product you want to update 
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            //if there is somehting there it will automatically update the list 
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found!");
            }

        }

        //return a single product 
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            //if there is somehting there it will automatically update the list 
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found!");
            }
        }
        //returns a list that can ebe queried
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();

        }
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            //if there is somehting there it will automatically update the list 
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

    }
}
