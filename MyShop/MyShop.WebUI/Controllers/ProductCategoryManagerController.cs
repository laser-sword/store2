using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    { //cretae an instance of your product repsoitory 
        ProductCategoryRepository context;
        //then create a contructor to initialize thats product repositry
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();

        }

        // GET: ProductManager
        //return a list of products 
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        //create method to create our product
        //first page is to create page to add details
        //2nd page is to post those details in 
        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);
            }
            else
            {
                context.Insert(productCategories);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            //first try to find the product in teh database by using teh find method
            ProductCategory productCategories = context.Find(Id);
            if (productCategories == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(productCategories);
            }

        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategories, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategories);
                }
                //if all ok pass through the info to all tyhe properties of the product
                productCategoryToEdit.Category = productCategories.Category;
             

                //finally commit changes
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
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