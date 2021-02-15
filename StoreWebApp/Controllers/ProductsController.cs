using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApp.Models;
using StoreWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IProduct _product;
        public ProductsController(StoreDbContext context, IProduct product)
        {
            _context = context;
            _product = product;
        }

        #region GetProductsList
        // GET: api/Products
        [HttpGet("/api/products")]
        public ActionResult GetProducts()
        {
            try
            {
                var products = _product.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new JsonResult(false);
            }
        }
        #endregion

        #region GetProductById
        // GET: api/Products/5
        [HttpGet("/api/Products/{id}")]
        public ActionResult GetProductById(int id)
        {
            try
            {
                var product = _product.GetProductById(id);
                return new JsonResult(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new JsonResult(false);
            }
        }
        #endregion

        #region PutProduct
        // PUT: api/Products/5
        [HttpPut("/api/Products/{id}")]
        public ActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return new JsonResult("Product Id not valid");
            }

            try
            {
                _product.EditProduct(product);

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new JsonResult(false);
            }
        }
        #endregion

        #region PostProduct
        // POST: api/Products
        [HttpPost("PostProduct")]
        public ActionResult PostProduct(Product product)            
        {
            try
            {
                _product.AddProduct(product);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new JsonResult(false);
            }
        }
        #endregion

        #region DeleteProduct
        // DELETE: api/Products/5
        [HttpDelete("/api/Products/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _product.DeleteProduct(id);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new JsonResult(false);
            }
        }
        #endregion
    }
}
