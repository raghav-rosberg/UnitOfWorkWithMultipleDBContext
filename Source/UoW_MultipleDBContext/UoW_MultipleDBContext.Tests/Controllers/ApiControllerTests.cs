using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Web.API.Controllers;
using UoW_MultipleDBContext.Web.API.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace UoW_MultipleDBContext.Tests.Controllers
{
    [TestFixture]
    public class ApiControllerTests
    {
        private Mock<IUnitOfWork<FirstDbContext>> _firstDbContextUoW;
        private Mock<IUnitOfWork<SecondDbContext>> _secondDbContextUoW;

        [SetUp]
        public void SetUp()
        {
            _firstDbContextUoW = new Mock<IUnitOfWork<FirstDbContext>>();
            _secondDbContextUoW = new Mock<IUnitOfWork<SecondDbContext>>();
        }

        [Test]
        public async Task Index_Returns_Category_List()
        {
            // Arrange   
            IEnumerable<Category> fakeCategories = new List<Category>
            {
                new Category {Id = 1, Name = "Test1", Description = "Test1Desc"},
                new Category {Id = 2, Name = "Test2", Description = "Test2Desc"},
                new Category {Id = 3, Name = "Test3", Description = "Test3Desc"}
            }.AsEnumerable();
            _firstDbContextUoW.Setup(x => x.CategoryRepository.GetAll()).Returns(fakeCategories);
            var controller = new CategoryController(_firstDbContextUoW.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var httpcontentResult = await controller.GetAll();
            
            // Assert
            Assert.IsNotNull(httpcontentResult);
            Assert.IsNotNull(httpcontentResult.Content);
            Assert.AreEqual(HttpStatusCode.Found, httpcontentResult.StatusCode);
            Assert.IsInstanceOf<ObjectContent<IEnumerable<Category>>>(httpcontentResult.Content);

            Assert.That(httpcontentResult.Content, Is.TypeOf(typeof(ObjectContent<IEnumerable<Category>>)));
            var contentResult = await httpcontentResult.Content.ReadAsAsync<IEnumerable<Category>>();
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(3, contentResult.Count());
        }

        [Test]
        public async Task Index_Returns_Department_List()
        {
            // Arrange   
            IEnumerable<Department> fakeDepartments = new List<Department>
            {
                new Department {Id = 1, Name = "Test1"},
                new Department {Id = 2, Name = "Test2"},
                new Department {Id = 3, Name = "Test3"}
            }.AsEnumerable();
            _secondDbContextUoW.Setup(x => x.DepartmentRepository.GetAll()).Returns(fakeDepartments);
            var controller = new DepartmentController(_secondDbContextUoW.Object);

            // Act
            var httpcontentResult = await controller.GetAll();

            // Assert
            Assert.IsNotNull(httpcontentResult);
            Assert.IsNotNull(httpcontentResult.Content);
            Assert.AreEqual(HttpStatusCode.Found, httpcontentResult.StatusCode);
            Assert.IsInstanceOf<ObjectContent<IEnumerable<Department>>>(httpcontentResult.Content);

            Assert.That(httpcontentResult.Content, Is.TypeOf(typeof(ObjectContent<IEnumerable<Department>>)));
            var contentResult = await httpcontentResult.Content.ReadAsAsync<IEnumerable<Department>>();
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(3, contentResult.Count());
        }
    }
}