using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.DepartmentService;
using UoW_MultipleDBContext.Web.Async.Models;

namespace UoW_MultipleDBContext.Web.Async.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: /Department/
        public async Task<ActionResult> Index()
        {
            var departments = await _departmentService.GetAllAsync();
            var departmentModelList = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(departments);
            return View(departmentModelList.ToList());
        }

        // GET: /Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Department department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
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
        public async Task<ActionResult> Create(DepartmentModel model) //FormCollection collection
        {
            try
            {
                // TODO: Add insert logic here
                if (model == null)
                    return View();
                var department = Mapper.Map<DepartmentModel, Department>(model);
                await _departmentService.InsertAsync(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            var departmentModel = Mapper.Map<Department, DepartmentModel>(department);
            return View(departmentModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, DepartmentModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                var department = Mapper.Map<DepartmentModel, Department>(model);
                await _departmentService.UpdateAsync(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public async Task<ActionResult> Delete(int id, DepartmentModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                await _departmentService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}