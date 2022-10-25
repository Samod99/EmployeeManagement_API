using EmployeeManagement_API.Models;
using EmployeeManagement_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : Controller
    {
        [HttpPost]
        public Employee insertEmployee([FromBody]Employee employee)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            Response response = employeeServices.insertEmployee(employee);
            return (Employee)response.content;
        }

        [HttpPut]
        [Route("{id}")]
        public Employee updateEmployee([FromRoute]int id, [FromBody]Employee employee)
        {
            employee.id = id;
            EmployeeServices employeeServices = new EmployeeServices();
            Response response = employeeServices.updateEmployee(employee);
            return (Employee)response.content;
        }

        [HttpDelete]
        [Route("{id}")]
        public Response deleteEmployee([FromRoute]int id)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            return employeeServices.deleteEmployee(id);
        }

        [HttpGet]
        public List<Employee> getAllEmployees()
        {
            EmployeeServices employeeServices =new EmployeeServices();
            Response response = employeeServices.getAllEmployees();
            return (List<Employee>)response.content;
        }

        [HttpGet]
        [Route("{id}")]
        public Employee getEmployeesById([FromRoute]int id)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            Response response = employeeServices.getEmployeesById(id);
            return (Employee)response.content;
        }
    }
}
