using System.Threading.Tasks;
using System.Web.Mvc;
using UoW_MultipleDBContext.Web.Core;

namespace UoW_MultipleDBContext.Web.Async.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly IApiPath _apiPath;

        public HomeController(IApiHelper apiHelper, IApiPath apiPath)
        {
            _apiHelper = apiHelper;
            _apiPath = apiPath;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            await _apiHelper.ApiLogin("test", "test");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}