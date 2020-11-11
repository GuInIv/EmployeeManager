using AutoMapper;
using EmployeeManager.ServerApp.Models;
using EmployeeManager.ServerApp.Models.BindingTargets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager.ServerApp.Controllers
{
    [Route("api/[controller]")]
    //[AutoValidateAntiforgeryToken]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IMapper mapper;
        DataContext context;
        public EmployeesController(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<EmployeeData> GetEmployees(bool related = false)
        {
            System.Linq.IQueryable<Employee> query = context.Employees;

            if (!related)
            {
                return query.Select(mapper.Map<Employee, EmployeeData>);
            }

            query = query.Include(e => e.Position);
            List<Employee> data = query.ToList();
            data.ForEach(e =>
            {
                if (e.Position != null)
                {
                    e.Position.Employees = null;
                }
            });

            return data.Select(mapper.Map<Employee, EmployeeData>);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeData employeeData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = mapper.Map<EmployeeData, Employee>(employeeData);

            if (employeeMap.Position != null && employeeMap.Position.Id != 0)
            {
                context.Attach(employeeMap.Position);
            }

            try
            {
                context.Employees.Add(employeeMap);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest(ModelState);
            }
            return Ok(employeeMap.Id);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(long id, [FromBody] EmployeeData employeeData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            employeeData.Id = id;
            var employeeMap = mapper.Map<EmployeeData, Employee>(employeeData);

            try
            {
                context.Employees.Update(employeeMap);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

	[HttpPost("GetEmployeeByParam")]
        public long PostOrUpdate([FromBody] EmployeeData employeeModel)
        {
            if (ModelState.IsValid)
            {
                var employeeData = mapper.Map<EmployeeData, Employee>(employeeModel);
                context.Employees.Attach(employeeData);
                var employee = context.Employees
                    .FirstOrDefault(x => x.LastName.Equals(employeeData.LastName)
                    && x.FirstName.Equals(employeeData.FirstName)
                    && !x.Position.Name.Equals(employeeData.Position.Name)
                    && !x.TerminationDate.HasValue
                    && employeeData.TerminationDate.HasValue);
                if (employee != null)
                {
                    return 0;
                }

                employee = context.Employees
                    .FirstOrDefault(x => x.LastName.Equals(employeeData.LastName)
                    && x.FirstName.Equals(employeeData.FirstName)
                    && employeeData.TerminationDate.HasValue);
                if (employee != null)
                {
                    return employee.Id;
                }
            }

            return 0;
        }
    }
}