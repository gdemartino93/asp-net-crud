using asp_net_crud.Data;
using asp_net_crud.Models;
using asp_net_crud.Models.Domain;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_net_crud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext )
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel request)
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
            await mvcDemoDbContext.Employees.AddAsync( employee );
            await mvcDemoDbContext.SaveChangesAsync(); // salviamo le modifiche nel db
            return RedirectToAction("Index"); // dopo aver inviato il form ritorna la vista index 
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(id);
            if( employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return await Task.Run(() => View("View", viewModel));

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel request)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(request.Id);
            if( employee != null )
            {
                employee.Name = request.Name;
                employee.Email = request.Email;
                employee.Salary = request.Salary;
                employee.DateOfBirth = request.DateOfBirth;
                employee.Department = request.Department;
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
    }
}
