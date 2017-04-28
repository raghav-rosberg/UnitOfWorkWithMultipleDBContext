using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using UoW_MultipleDBContext.Web.Async.Models;
using UoW_MultipleDBContext.Web.Core;

namespace UoW_MultipleDBContext.Web.Async.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly IApiPath _apiPath;

        public DepartmentController(IApiHelper apiHelper, IApiPath apiPath)
        {
            _apiHelper = apiHelper;
            _apiPath = apiPath;
        }

        // GET: /Department/
        public async Task<ActionResult> Index()
        {
            var departments = await _apiHelper.GetDataFromAPi(_apiPath.GetAllDepartments);
            var allDepartmentModelList = JsonConvert.DeserializeObject<IEnumerable<DepartmentModel>>(await departments.Content.ReadAsStringAsync());
            return View(allDepartmentModelList);
        }

        // GET: /Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var department = await _apiHelper.GetDataFromAPi(_apiPath.GetDepartmentbyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            var departmentModel = JsonConvert.DeserializeObject<DepartmentModel>(await department.Content.ReadAsStringAsync());
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
                await _apiHelper.PostDataToAPi(_apiPath.CreateDepartment, JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var department = await _apiHelper.GetDataFromAPi(_apiPath.GetDepartmentbyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            if (department == null)
            {
                return HttpNotFound();
            }
            var categoryModel = JsonConvert.DeserializeObject<DepartmentModel>(await department.Content.ReadAsStringAsync());
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, DepartmentModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                await _apiHelper.PostDataToAPi(_apiPath.UpdateDepartment, JsonConvert.SerializeObject(model));
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
            var department = await _apiHelper.GetDataFromAPi(_apiPath.GetDepartmentbyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            if (department == null)
            {
                return HttpNotFound();
            }
            var categoryModel = JsonConvert.DeserializeObject<DepartmentModel>(await department.Content.ReadAsStringAsync());
            return View(categoryModel);
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
                await _apiHelper.PostDataToAPi(_apiPath.DeleteDepartment, JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}