﻿using asp_net_crud.Data;
using asp_net_crud.Models;
using asp_net_crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;

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
            return RedirectToAction("Add"); // dopo aver inviato il form ritorna la vista add 
        }
    }
}
