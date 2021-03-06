using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using Models;
using Data;
using Serilog;
using System.Collections.Generic;

namespace WebAPI
{
    [Route("[Controller]")]
    [ApiController]
    public class EmployeeInformationController : Controller
    {
        //Dependency Injection
        private readonly IEmployeeInformationRepository _repo;
        public EmployeeInformationController(IEmployeeInformationRepository p_repo) { _repo = p_repo; }

        // GET: Employee/Get/All
        [HttpGet("Get/All")]//Gets list of all employees
        public IActionResult GetAll()
        {
            try
            {
                List<EmployeeInformation> e = _repo.GetAll().ToList();
                if (e.Count == 0)
                    throw new FileNotFoundException("No data found");
                return Ok(_repo.GetAllEmployee());
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Invalid get all request.");//bad request messages logged into separate file
            }
        }

        // GET: Employee/Get/{Id}
        [HttpGet("Get/{id}")]//Gets employee by Id
        public IActionResult GetById(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new InvalidDataException("Invalid Id");
                return Ok(_repo.GetEmployeeInformationById(id));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Not a valid ID");
            }
        }

        // GET: Employee/Verify/{p_email}
        [HttpGet("Verify/{p_email}")]//Gets employee verified by email
        public IActionResult VerifyUser(string p_email)
        {
            try
            {
                return Ok(_repo.VerifyEmail(p_email));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Not a current user");
            }
        }

        // POST Employee/Add
        [HttpPost("Add")]//Adds employee info
        public IActionResult Add([FromBody] EmployeeInformation p_employee)
        {
            try
            {
                if (p_employee == null)
                    throw new InvalidDataException("Invalid data!");
                _repo.Create(p_employee);
                _repo.Save();
                return Created("Employee/Add", p_employee);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Invalid input.");
            }
        }

        // DELETE employee/delete/{id}
        [HttpDelete("Delete/{id}")]//Deletes employee by Id
        public IActionResult Delete(int id)
        {
            try
            {
                var topic = _repo.GetById(id);
                if (topic == null)
                    throw new InvalidDataException("Delete failed!");
                _repo.Delete(topic);
                _repo.Save();
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Not a valid Id");
            }
        }

        // PUT Employee/Update/{id}
        [HttpPut("Update/{id}")]//Updates employee info by Id
        public IActionResult Update(int id, [FromBody] EmployeeInformation p_employee)
        {
            try
            {
                p_employee.Id = id;
                _repo.Update(p_employee);
                _repo.Save();
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return BadRequest("Invalid Input");
            }
        }
    }
}