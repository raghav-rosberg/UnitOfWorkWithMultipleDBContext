using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Web.API.Models;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Data.DBContexts;
using System.Threading.Tasks;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private readonly IUnitOfWork<SecondDbContext> _unitOfWork;

        public DepartmentController(IUnitOfWork<SecondDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var data = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("GetById")]
        [HttpGet]
        // GET /api/category/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            var data = await _unitOfWork.DepartmentRepository.GetAsync(x => x.Id == id);
            return Request.CreateResponse(HttpStatusCode.Found, data);
        }

        [Route("Insert")]
        [HttpPost]
        // POST /api/category
        public HttpResponseMessage Insert(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Insert(entity);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Update")]
        [HttpPost]
        // PUT /api/category/5
        public HttpResponseMessage Update(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Update(entity);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.Accepted) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("Delete")]
        [HttpPost]
        // DELETE /api/category/5
        public HttpResponseMessage Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Delete(entity.Id);
            var result = _unitOfWork.Commit();
            return result == 1 ? Request.CreateResponse(HttpStatusCode.MovedPermanently) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}