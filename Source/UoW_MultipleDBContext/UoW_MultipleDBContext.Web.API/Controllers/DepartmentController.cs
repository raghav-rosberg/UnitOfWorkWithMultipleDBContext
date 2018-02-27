using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private readonly IDBTwoRepositories dBTwoRepositories;
        public DepartmentController(IDBTwoRepositories dBTwoRepositories)
        {
            this.dBTwoRepositories = dBTwoRepositories;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var data = await dBTwoRepositories.DepartmentRepository.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetById")]
        [HttpGet]
        // GET /api/category/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            var data = await dBTwoRepositories.DepartmentRepository.GetAsync(x => x.Id == id);
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("Insert")]
        [HttpPost]
        // POST /api/category
        public HttpResponseMessage Insert(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBTwoRepositories.DepartmentRepository.Insert(entity);
            var result = dBTwoRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Update")]
        [HttpPost]
        // PUT /api/category/5
        public HttpResponseMessage Update(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBTwoRepositories.DepartmentRepository.Update(entity);
            var result = dBTwoRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Accepted) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Delete")]
        [HttpPost]
        // DELETE /api/category/5
        public HttpResponseMessage Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            dBTwoRepositories.DepartmentRepository.Delete(entity.Id);
            var result = dBTwoRepositories.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.MovedPermanently) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}