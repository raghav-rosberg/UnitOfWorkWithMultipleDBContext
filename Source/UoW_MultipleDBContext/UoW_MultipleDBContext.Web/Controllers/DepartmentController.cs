using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.Infrastructure.Interface;
using UoW_MultipleDBContext.Web.Models;

namespace UoW_MultipleDBContext.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        // GET: /Category/

        public ActionResult Index()
        {
            var departments = _departmentService.GetAll().ToList();
            var categoryModelList = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(departments);
            return View(categoryModelList);
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            var department = _departmentService.GetById(id);
            var departmentModel = Mapper.Map<Department, DepartmentModel>(department);
            return View(departmentModel);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            var model = new DepartmentModel();
            return View(model);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(DepartmentModel model)//FormCollection collection
        {
            try
            {
                // TODO: Add insert logic here
                if (model == null)
                    return View(model);
                var department = Mapper.Map<DepartmentModel, Department>(model);
                _departmentService.Insert(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            var department = _departmentService.GetById(id);
            var departmentModel = Mapper.Map<Department, DepartmentModel>(department);
            return View(departmentModel);
        }


        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            var departmentModel = Mapper.Map<Department, DepartmentModel>(department);
            return View(departmentModel);

        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, DepartmentModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                var category = _departmentService.GetById(id);
                _departmentService.Delete(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
