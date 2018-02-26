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

namespace UoW_MultipleDBContext.Tests.Unit.Controllers
{
    [TestFixture]
    public class ApiControllerTests
    {
        //private Mock<IUnitOfWork<FirstDbContext>> _firstDbContextUoW;
        private Mock<IDBOneRepositories> dbOneRepositories;
        //private Mock<IUnitOfWork<SecondDbContext>> _secondDbContextUoW;
        private Mock<IDBTwoRepositories> dbTwoRepositories;

        [SetUp]
        public void SetUp()
        {
            //_firstDbContextUoW = new Mock<IUnitOfWork<FirstDbContext>>();
            this.dbOneRepositories = new Mock<IDBOneRepositories>();
            //_secondDbContextUoW = new Mock<IUnitOfWork<SecondDbContext>>();
            this.dbTwoRepositories = new Mock<IDBTwoRepositories>();
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
             
            dbOneRepositories.Setup(x => x.CategoryRepository.GetAllAsync())
                 .ReturnsAsync(fakeCategories.ToList());
            var controller = new CategoryController(dbOneRepositories.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var httpcontentResult = await controller.GetAll();

            // Assert
            Assert.IsNotNull(httpcontentResult);
            Assert.IsNotNull(httpcontentResult.Content);
            Assert.AreEqual(HttpStatusCode.Found, httpcontentResult.StatusCode);
            Assert.IsInstanceOf<ObjectContent<List<Category>>>(httpcontentResult.Content);

            Assert.That(httpcontentResult.Content, Is.TypeOf(typeof(ObjectContent<List<Category>>)));
            var contentResult = await httpcontentResult.Content.ReadAsAsync<List<Category>>();
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
            dbTwoRepositories.Setup(x => x.DepartmentRepository.GetAllAsync())
                  .ReturnsAsync(fakeDepartments.ToList());
            var controller = new DepartmentController(dbTwoRepositories.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act
            var httpcontentResult = await controller.GetAll();

            // Assert
            Assert.IsNotNull(httpcontentResult);
            Assert.IsNotNull(httpcontentResult.Content);
            Assert.AreEqual(HttpStatusCode.Found, httpcontentResult.StatusCode);
            Assert.IsInstanceOf<ObjectContent<List<Department>>>(httpcontentResult.Content);

            Assert.That(httpcontentResult.Content, Is.TypeOf(typeof(ObjectContent<List<Department>>)));
            var contentResult = await httpcontentResult.Content.ReadAsAsync<List<Department>>();
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(3, contentResult.Count());
        }
    }
}