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
using UoW_MultipleDBContext.Data.Repositories;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly IDBOneRepositories dBOneRepositories;
        public CategoryController(
            IDBOneRepositories dBOneRepositories
            )
        {
            this.dBOneRepositories = dBOneRepositories;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var data = await dBOneRepositories.CategoryRepository.GetAllAsync();//_unitOfWork.CategoryRepository.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            var data = await dBOneRepositories.CategoryRepository.GetAsync(x => x.Id == id);//_unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetByName")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByName(string key)
        {
            var data = await dBOneRepositories.CategoryRepository.GetAsync(x => x.Name.Contains(key));//_unitOfWork.CategoryRepository.GetAsync(x => x.Name.Contains(key));
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetCategoryWithExpenses")]
        [HttpGet]
        public HttpResponseMessage GetCategoryWithExpenses()
        {
            var data = dBOneRepositories.CategoryRepository.GetCategoryWithExpenses();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("Insert")]
        [HttpPost]
        public HttpResponseMessage Insert(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBOneRepositories.CategoryRepository.Insert(entity);
            var result = dBOneRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBOneRepositories.CategoryRepository.Update(entity);
            var result = dBOneRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Accepted) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Delete")]
        [HttpPost]
        // DELETE /api/category/5
        public HttpResponseMessage Delete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBOneRepositories.CategoryRepository.Delete(entity.Id);
            var result = dBOneRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.MovedPermanently) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}