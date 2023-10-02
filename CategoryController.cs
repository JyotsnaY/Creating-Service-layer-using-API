using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace QuickKartServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        QuickKartRepository repository;
        public CategoryController(QuickKartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = repository.GetAllCategories();
            }
            catch(Exception ex)
            {
                categories = null;
            }
            return Json(categories);

        }
        [HttpGet]
        public JsonResult GetCategoryById(byte categoryId)
        {
            Category category = new Category();
            try
            {
               category = repository.GetCategoryById(categoryId);
            }
            catch (Exception)
            {

                category=null;
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult InsertCategory(Category category)
        { 
            bool status = false;
            string message;
            try
            {
                status = repository.AddCategory(category);
                if (status)
                {
                    message = "success , Category Id=" + category.CategoryId;
                }
                else
                {
                    message = "unsuccessfull";
                }
            }
            catch (Exception)
            {

                message = "error";
            }
            return Json(message);
        }
        [HttpPut]
        public JsonResult UpdateCategory(byte categoryId, string categoryName)
        {
            bool status = false;
            try
            {
                    status = repository.UpdateCategory(categoryId, categoryName);

            }
            catch (Exception)
            {
               status=false;
            }
            return Json(status);
        }
        [HttpDelete]
        public JsonResult DeleteCategory(byte categoryId) { 
            bool status = false;
            try
            {
                status = repository.DeleteCategory(categoryId);
            }
            catch (Exception)
            {

                status = false;
            }
            return Json(status);
        }
    }
}
