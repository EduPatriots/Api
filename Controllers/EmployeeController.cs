using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseFirstDWB_Sabado.Backend;
using DBFirstBack_End.DataAccess;
using DBFirstBack_End.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeesSC employeeService = new EmployeesSC();

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = employeeService.GetData().Select(s => new Employees
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDate = s.BirthDate,
                Address = s.Address
            }).ToList();
            return Ok(employees);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var employee = employeeService.GetDataByID(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }

        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel newEmployee)
        {
            employeeService.AddEmployee(newEmployee);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string newName)
        {
            try
            {
                employeeService.UpdateEmployeeFirstNameById(id, newName);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                employeeService.DeleteEmployeeByID(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }

        }

        #region helpers
        private IActionResult ThrowInternalErrorServer(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        #endregion
    }
}
