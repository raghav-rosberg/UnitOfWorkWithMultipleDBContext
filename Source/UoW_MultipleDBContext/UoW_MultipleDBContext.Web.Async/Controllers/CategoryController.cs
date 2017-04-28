using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using UoW_MultipleDBContext.Web.Async.Models;
using UoW_MultipleDBContext.Web.Core;
using Newtonsoft.Json;

namespace UoW_MultipleDBContext.Web.Async.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly IApiPath _apiPath;

        public CategoryController(IApiHelper apiHelper, IApiPath apiPath)
        {
            _apiHelper = apiHelper;
            _apiPath = apiPath;
        }

        // GET: /Department/
        public async Task<ActionResult> Index(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                var categories = await _apiHelper.GetDataFromAPi(_apiPath.GetAllCategories);
                var allCategoryModelList = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await categories.Content.ReadAsStringAsync());
                return View(allCategoryModelList);
            }

            string key = keyword;
            var category = await _apiHelper.GetDataFromAPi("", new[]
                        {
                            new KeyValuePair<string, string>("key", keyword)
                        });
            var categoryModelList = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await category.Content.ReadAsStringAsync());

            return View(categoryModelList);
        }

        // GET: /Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _apiHelper.GetDataFromAPi(_apiPath.GetCategorybyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryModel = JsonConvert.DeserializeObject<CategoryModel>(await category.Content.ReadAsStringAsync());
            return View(categoryModel);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            var model = new CategoryModel();
            return View(model);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public async Task<ActionResult> Create(CategoryModel model) //FormCollection collection
        {
            try
            {
                // TODO: Add insert logic here
                if (model == null)
                    return View();
                await _apiHelper.PostDataToAPi(_apiPath.CreateCategory, JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var category = await _apiHelper.GetDataFromAPi(_apiPath.GetCategorybyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryModel = JsonConvert.DeserializeObject<CategoryModel>(await category.Content.ReadAsStringAsync());
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, CategoryModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                await _apiHelper.PostDataToAPi(_apiPath.UpdateCategory, JsonConvert.SerializeObject(model));
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
            var category = await _apiHelper.GetDataFromAPi(_apiPath.GetCategorybyId, new[]
                        {
                            new KeyValuePair<string, string>("id", id.ToString())
                        });
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryModel = JsonConvert.DeserializeObject<CategoryModel>(await category.Content.ReadAsStringAsync());

            //CategoryModel
            return View(categoryModel);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public async Task<ActionResult> Delete(int id, CategoryModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                await _apiHelper.PostDataToAPi(_apiPath.DeleteCategory, JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}