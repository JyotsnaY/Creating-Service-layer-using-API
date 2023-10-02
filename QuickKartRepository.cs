using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using QuickKartDataAccessLayer.Models;

namespace QuickKartDataAccessLayer
{
    public class QuickKartRepository
    {

        private QuickKartDBContext context;

        public QuickKartDBContext Context { get { return context; } }

        public QuickKartRepository(QuickKartDBContext dbContext)
        {
            context = dbContext;
        }
        public QuickKartRepository()
        {
            context = new QuickKartDBContext();
        }

        #region Library for Services Demo

        #region-To get all product details
        public List<Product> GetAllProducts()
        {
            List<Product> lstProducts = new List<Product>();
            try
            {
                lstProducts = (from p in context.Products
                               orderby p.ProductId
                               select p).ToList();
            }
            catch (Exception)
            {
                lstProducts = new List<Product>();
            }
            return lstProducts;
        }
        #endregion

        #region-To get a product detail by using ProductId
        public Product GetProductById(string productId)
        {
            Product product;
            try
            {
                product = (from p in context.Products
                           where p.ProductId.Equals(productId)
                           select p).FirstOrDefault();
            }
            catch (Exception)
            {
                product = null;
            }
            return product;
        }
        #endregion

        #region- To generate new product id
        public string GenerateNewProductId()
        {
            string productId;
            try
            {
                productId = (from p in context.Products
                             select QuickKartDBContext.GenerateProductID()).FirstOrDefault();
            }
            catch (Exception)
            {
                productId = null;
            }
            return productId;
        }
        #endregion

        #region- To add a new product using Params
        public bool AddProduct(string productName, byte categoryId, decimal price, int quantityAvailable, out string productId)
        {
            bool status = false;
            productId = null;
            try
            {
                Product prodObj = new Product();
                prodObj.ProductId = GenerateNewProductId();
                productId = prodObj.ProductId;
                prodObj.ProductName = productName;
                prodObj.CategoryId = categoryId;
                prodObj.Price = price;
                prodObj.QuantityAvailable = quantityAvailable;
                context.Products.Add(prodObj);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                productId = null;
                status = false;

            }
            return status;
        }
        #endregion

        #region- To add a new product Using Entity Models 
        public bool AddProduct(Product product)
        {
            bool status = false;
            try
            {
                product.ProductId = GenerateNewProductId();
                context.Products.Add(product);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {

                status = false;

            }
            return status;
        }

        #endregion

        #region- To update the existing product details using Entity Models
        public bool UpdateProduct(Product products)
        {
            bool status = false;
            try
            {
                var product = (from prdct in context.Products
                               where prdct.ProductId == products.ProductId
                               select prdct).FirstOrDefault<Product>();
                if (product != null)
                {
                    product.ProductId = products.ProductId;
                    product.ProductName = products.ProductName;
                    product.Price = products.Price;
                    product.QuantityAvailable = products.QuantityAvailable;
                    product.CategoryId = products.CategoryId;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region-To delete a existing product
        public bool DeleteProduct(string prodId)
        {
            bool status = false;
            try
            {
                var product = (from prdct in context.Products
                               where prdct.ProductId == prodId
                               select prdct).FirstOrDefault<Product>();
                context.Products.Remove(product);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region Library for Services Exercise

        #region-To get all category details
        public List<Category> GetAllCategories()
        {
            List<Category> lstCategories = null;
            try
            {
                lstCategories = (from c in context.Categories
                                 orderby c.CategoryId ascending
                                 select c).ToList<Category>();
            }
            catch (Exception)
            {
                lstCategories = null;
            }
            return lstCategories;
        }
        #endregion

        #region get a category detail by using CategoryId
        public Category GetCategoryById(byte categoryId)
        {
            Category categories = new Category();
            try
            {

                categories = (from c in context.Categories
                              where c.CategoryId.Equals(categoryId)
                              select c).FirstOrDefault();
            }
            catch (Exception)
            {
                categories = null;
            }
            return categories;
        }
        #endregion

        #region-To generate new category id
        public byte GenerateNewCategoryId()
        {
            byte categoryId;
            try
            {
                var newCategoryId = (from p in context.Categories select QuickKartDBContext.GenerateCategoryID()).FirstOrDefault();
                categoryId = Convert.ToByte(newCategoryId);
            }
            catch (Exception)
            {
                categoryId = 0;
            }
            return categoryId;

        }
        #endregion

        #region-To add a new category using EF Models
        public bool AddCategory(Category category)
        {
            bool status = false;
            try
            {
               // category.CategoryId = GenerateNewCategoryId();
                context.Categories.Add(category);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region-To update the existing category details 
        public bool UpdateCategory(byte categoryId, string categoryName)
        {
            bool status = false;
            try
            {
                var category = context.Categories.Find(categoryId);

                if (category != null)
                {
                    category.CategoryName = categoryName;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region-To delete a existing category
        public bool DeleteCategory(byte categID)
        {
            bool status = false;
            try
            {
                var category = (from ctgry in context.Categories
                                where ctgry.CategoryId == categID
                                select ctgry).FirstOrDefault<Category>();
                context.Categories.Remove(category);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #endregion
    }
}
#endregion