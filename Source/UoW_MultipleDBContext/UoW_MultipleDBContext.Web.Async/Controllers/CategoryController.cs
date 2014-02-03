using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.CategoryService;
using UoW_MultipleDBContext.Web.Async.Models;

namespace UoW_MultipleDBContext.Web.Async.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Department/
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryModelList = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(categories);
            return View(categoryModelList.ToList());
        }

        // GET: /Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categoryModel = Mapper.Map<Category, CategoryModel>(category);
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
                var category = Mapper.Map<CategoryModel, Category>(model);
                await _categoryService.InsertAsync(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var categoryModel = Mapper.Map<Category, CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, CategoryModel model)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");
                var category = Mapper.Map<CategoryModel, Category>(model);
                await _categoryService.UpdateAsync(category);
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
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
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
                await _categoryService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}