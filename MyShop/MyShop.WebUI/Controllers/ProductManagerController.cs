using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //cretae an instance of your product repsoitory 
        //ProductRepository context;
        //create a new inMemoryRepository to take in teh new baseclass
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategories;


        //ProductCategoryRepository productCategories;
        //then create a contructor to initialize thats product repositry
        //so you can send through the product and list of product categories

        public ProductManagerController() {
            //context = new ProductRepository();
            ////initialize the repository
            //productCategories = new ProductCategoryRepository();

            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();


        }

        // GET: ProductManager
        //return a list of products 
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //create method to create our product
        //first page is to create page to add details
        //2nd page is to post those details in 
        public ActionResult Create() {
            //create a reference to the productmanager viewmodel
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            //put in an empty product
            viewModel.Product = new Product();
            //then add a list of product categories that you get from the database
            viewModel.ProductCategories = productCategories.Collection();        
          
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product) {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id) {
            //first try to find the product in teh database by using teh find method
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();

            }
            else {

                //same sa create do it for edit as well
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                //
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
                //since you have changed what you are passing in the view, you now need to update the view pages
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id) {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else {
                if (!ModelState.IsValid) {
                    return View(product);
                }
                //if all ok pass through the info to all tyhe properties of the product
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                //finally commit changes
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(string Id) {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}