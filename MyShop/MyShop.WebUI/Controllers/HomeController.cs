using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;


        //ProductCategoryRepository productCategories;
        //then create a contructor to initialize thats product repositry
        //so you can send through the product and list of product categories

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            //context = new ProductRepository();
            ////initialize the repository
            //productCategories = new ProductCategoryRepository();


            //context = new InMemoryRepository<Product>();
            //productCategories = new InMemoryRepository<ProductCategory>();
            context = productContext;
            productCategories = productCategoryContext;

        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Details(string Id) {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else {
                return View(product);
            }
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}