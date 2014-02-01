using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.CategoryService;
using UoW_MultipleDBContext.Web.API.Models;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IQueryable<CategoryModel> Get()
        {
            var category= _categoryService.GetAll();
            var entityList = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(category);
            return entityList.AsQueryable();
        }

        // GET /api/category/5
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST /api/category
        public IHttpActionResult Post(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<CategoryModel, Category>(category);
                    _categoryService.Insert(entity);
                    var response = CreatedAtRoute("DefaultApi", new {id = category.Id}, category);
                    return response;
                }
                catch (Exception ex)
                {
                    var responseMessage = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                    return ResponseMessage(responseMessage);
                }
            }
            return BadRequest(ModelState);
        }

        // PUT /api/category/5
        public IHttpActionResult Put(int id, CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<CategoryModel, Category>(category);
                    _categoryService.Update(entity);
                    return Ok(category);
                }
                catch (Exception ex)
                {
                    var responseMessage = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                    return ResponseMessage(responseMessage);
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE /api/category/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var responseMessage = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                return ResponseMessage(responseMessage);
            }
        }
    }
}
