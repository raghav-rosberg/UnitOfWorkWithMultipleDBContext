using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Web.API.Models;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Data.DBContexts;
using System.Threading.Tasks;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork<FirstDbContext> _unitOfWork;
        
        public CategoryController(IUnitOfWork<FirstDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var data = await _unitOfWork.CategoryRepository.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            var data = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetByName")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByName(string key)
        {
            var data = await _unitOfWork.CategoryRepository.GetAsync(x => x.Name.Contains(key));
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetCategoryWithExpenses")]
        [HttpGet]
        public HttpResponseMessage GetCategoryWithExpenses()
        {
            var data = _unitOfWork.CategoryRepository.GetCategoryWithExpenses();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("Insert")]
        [HttpPost]
        public HttpResponseMessage Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Insert(entity);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Update(entity);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Accepted) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Delete")]
        [HttpPost]
        // DELETE /api/category/5
        public HttpResponseMessage Delete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.CategoryRepository.Delete(entity.Id);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.MovedPermanently) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}