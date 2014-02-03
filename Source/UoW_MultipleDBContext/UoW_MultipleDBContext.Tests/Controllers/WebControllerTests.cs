using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Service.CategoryService;
using UoW_MultipleDBContext.Service.DepartmentService;
using UoW_MultipleDBContext.Web.Controllers;
using UoW_MultipleDBContext.Web.Models;

namespace UoW_MultipleDBContext.Tests.Controllers
{
    [TestFixture]
    public class WebControllerTests
    {
        private Mock<IUnitOfWork<FirstDbContext>> _firstDbContextUoW;
        private Mock<IUnitOfWork<SecondDbContext>> _secondDbContextUoW;
        private ICategoryService _categoryService;
        private IDepartmentService _departmentService;

        [SetUp]
        public void SetUp()
        {
            _firstDbContextUoW = new Mock<IUnitOfWork<FirstDbContext>>();
            _secondDbContextUoW = new Mock<IUnitOfWork<SecondDbContext>>();
            _categoryService = new CategoryService(_firstDbContextUoW.Object);
            _departmentService = new DepartmentService(_secondDbContextUoW.Object);
        }

        [Test]
        public void Index_Returns_Category_List()
        {
            // Arrange   
            IEnumerable<Category> fakeCategories = new List<Category>
            {
                new Category {Id = 1, Name = "Test1", Description = "Test1Desc"},
                new Category {Id = 2, Name = "Test2", Description = "Test2Desc"},
                new Category {Id = 3, Name = "Test3", Description = "Test3Desc"}
            }.AsEnumerable();
            _firstDbContextUoW.Setup(x => x.CategoryRepository.GetAll()).Returns(fakeCategories);
            var controller = new CategoryController(_categoryService);

            // Act
            Mapper.CreateMap<Category, CategoryModel>();
            var result = (ViewResult) controller.Index();
            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOf(typeof (IEnumerable<CategoryModel>),
                result.ViewData.Model, "Wrong View Model");
            var categories = result.ViewData.Model as IEnumerable<CategoryModel>;
            if (categories != null) Assert.AreEqual(3, categories.Count(), "Got wrong number of Categories");
        }

        [Test]
        public void Index_Returns_Department_List()
        {
            // Arrange   
            IEnumerable<Department> fakeDepartments = new List<Department>
            {
                new Department {Id = 1, Name = "Test1"},
                new Department {Id = 2, Name = "Test2"},
                new Department {Id = 3, Name = "Test3"}
            }.AsEnumerable();
            _secondDbContextUoW.Setup(x => x.DepartmentRepository.GetAll()).Returns(fakeDepartments);
            var controller = new DepartmentController(_departmentService);

            // Act
            Mapper.CreateMap<Department, DepartmentModel>();
            var result = (ViewResult) controller.Index();
            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOf(typeof (IEnumerable<DepartmentModel>),
                result.ViewData.Model, "Wrong View Model");
            var departments = result.ViewData.Model as IEnumerable<DepartmentModel>;
            if (departments != null) Assert.AreEqual(3, departments.Count(), "Got wrong number of Departments");
        }
    }
}