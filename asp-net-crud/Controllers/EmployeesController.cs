using asp_net_crud.Models;
using asp_net_crud.Models.Domain;
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
        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel request)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Salary = request.Salary,
                DateOfBirth = request.DateOfBirth,
                Department = request.Department,
            };
            
        }
    }
}
