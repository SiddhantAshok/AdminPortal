using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public ApplicationDBContext dbContext { get; }

        public EmployeesController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
            //return 
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            //Employee employeeEntity = dbContext.Employees.FirstOrDefault(e => e.Id == Id);
            Employee employeeEntity = dbContext.Employees.Find(id);
            if (employeeEntity != null)
            {
                return Ok(employeeEntity);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            dbContext.Employees.Add(employeeEntity);

            dbContext.SaveChanges();
            return Created("", employeeEntity);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            //Employee employeeEntity = dbContext.Employees.FirstOrDefault(e => e.Id == employee.Id);
            Employee employeeEntity = dbContext.Employees.Find(employee.Id);
            if (employeeEntity != null)
            {

                employeeEntity.Name = employee.Name; employeeEntity.Email = employee.Email;
                employeeEntity.Phone = employee.Phone; employeeEntity.Salary = employee.Salary;

                dbContext.Employees.Update(employeeEntity);
                dbContext.SaveChanges();
                return Ok(employeeEntity);
            }
            else
            {
                return NotFound();
            }

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            //Employee employeeEntity = dbContext.Employees.FirstOrDefault(e => e.Id == Id);
            Employee employeeEntity = dbContext.Employees.Find(id);

            if (employeeEntity != null)
            {
                dbContext.Employees.Remove(employeeEntity);
                dbContext.SaveChanges();
                return Ok(employeeEntity);

            }else{
                return NotFound();
            }

        }
    }
}
