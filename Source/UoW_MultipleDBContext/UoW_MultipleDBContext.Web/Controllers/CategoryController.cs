using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.Infrastructure.Interface;
using UoW_MultipleDBContext.Web.Models;

namespace UoW_MultipleDBContext.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Category/

        public ActionResult Index()
        {
            var cagetories = _categoryService.GetAll().ToList();
            var categoryModelList = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(cagetories);
            return View(categoryModelList);
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            var category = _categoryService.GetById(id);
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
        public ActionResult Create(CategoryModel model)//FormCollection collection
        {
            try
            {
                // TODO: Add insert logic here
                if (model == null)
                    return View(model);
                var category = Mapper.Map<CategoryModel, Category>(model);
                _categoryService.Insert(category);
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
            var category = _categoryService.GetById(id);
            var categoryModel = Mapper.Map<Category, CategoryModel>(category);
            return View(categoryModel);
        }


        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            var categoryModel = Mapper.Map<Category, CategoryModel>(category);
            return View(categoryModel);
        }

    }
}
