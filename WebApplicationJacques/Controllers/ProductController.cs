using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationJacques.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            return GlobalConfig.Connection.GetProducts();
        }
         public ProductModel Get(int id)
        {
            return GlobalConfig.Connection.GetProductById(id);
        }
        public void Post(ProductModel product)
        {
            //String path = @"C:\Users\Jacques\Pictures\Camera Roll\Burger.jpg";
            String path = @"C:\Users\Jacques\Documents\Projects\ReactJs\singularpraticaljacques\public\img\product-8.png";
           
             product.Image = File.ReadAllBytes(path);
            GlobalConfig.Connection.CreateProduct(product);
        }
    }
}
