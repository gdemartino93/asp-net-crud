using Microsoft.AspNetCore.Mvc;

namespace asp_net_crud.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
