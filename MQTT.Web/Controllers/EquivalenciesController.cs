using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Web.Controllers
{
    [Authorize]
    public class EquivalenciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
