using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.DepartmentService;
using UoW_MultipleDBContext.Web.API.Models;

namespace UoW_MultipleDBContext.Web.API.Controllers
{
    public class DepartmentController: ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IQueryable<DepartmentModel> Get()
        {
            var department= _departmentService.GetAll();
            var entityList = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(department);
            return entityList.AsQueryable();
        }

        // GET /api/category/5
        public IHttpActionResult Get(int id)
        {
            var department = _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // POST /api/category
        public IHttpActionResult Post(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<DepartmentModel, Department>(department);
                    _departmentService.Insert(entity);
                    var response = CreatedAtRoute("DefaultApi", new { id = department.Id }, department);
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
        public IHttpActionResult Put(int id, DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<DepartmentModel, Department>(department);
                    _departmentService.Update(entity);
                    return Ok(department);
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
                _departmentService.Delete(id);
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