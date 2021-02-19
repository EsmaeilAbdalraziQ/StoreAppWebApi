using Microsoft.EntityFrameworkCore;
using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly StoreDbContext _db;
        public ProductRepository(StoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _db.Products.ToList();


            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _db.Products.Find(id);
            
            return product;
        }

        public void AddProduct(Product product)
        {
            product.LastUpdated = DateTime.Now;

            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            var Editedproduct = _db.Products.FirstOrDefault(f => f.ProductId == product.ProductId);

            //if (Editedproduct != null)
            //{
            //    _db.Entry(product).State = EntityState.Modified;
            //    Editedproduct.LastUpdated = DateTime.Now;

            Editedproduct.ProductName = product.ProductName;
            Editedproduct.ProductPrice = product.ProductPrice;
            Editedproduct.LastUpdated = DateTime.Now;

            //Editedproduct.Photo = product.Photo;

            _db.SaveChanges();
            //}
        }

        public bool DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();

            return true;
        }

        
    }
}
