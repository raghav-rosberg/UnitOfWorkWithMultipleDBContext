using System.Linq;
using System.Web.Mvc;
using UoW_MultipleDBContext.Service.Infrastructure.Interface;

namespace UoW_MultipleDBContext.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public HomeController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            var ds = _departmentService.GetAll().ToList();
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
