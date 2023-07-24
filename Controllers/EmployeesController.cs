using AutoMapper;
using EventHub.Dal;
using EventHub.DTO;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PublicPolicy")]
    public class EmployeesController : ControllerBase
    {

        private readonly ICommonRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(ICommonRepository<Employee> repository,IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }

        ///*
        // *this method is formated using IActionResult
        // *which takes the status code and type in ProducesResponseType
        // */

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Get()
        //{
        //  var employees = _employeeRepository.GetAll();
        //    if(employees.Count <= 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(employees);

        //}

        //[HttpGet("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetDetails(int id)
        //{
        //    var employee = _employeeRepository.GetDetails(id);

        //    return employee == null ? NotFound() : Ok(employee);
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = _employeeRepository.GetAll();

            //return employees == null ? NotFound() : Ok(employees);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult<EmployeeDto> GetDetails(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeDto>(employee));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Employee,Hr")]
        public async Task<ActionResult> Create(NewEmployeeDto employee)
        {
            var employeeModel = _mapper.Map<Employee>(employee);
            _employeeRepository.Insert(employeeModel);

            var result = _employeeRepository.SaveChanges();

            if (result > 0)
            {
                /*
                 * first parameter is actionName - the name of the action to use for generating the uri
                 * routeValue - the route data to use for generating the uri
                 * value - the content value to format in the entity body
                 */

                var employeeDetails = _mapper.Map<EmployeeDto>(employeeModel);
                return CreatedAtAction("GetDetails", new { id = employeeDetails.EmployeeId }, employeeDetails);
            }

            return BadRequest();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest | StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,Hr")]
        public ActionResult Update(Employee employee)
        {
            //var employeeId = _employeeRepository.GetDetails(employee.EmployeeId);
            //if(employeeId == null) return NotFound();

            _employeeRepository.Update(employee);
            var result = _employeeRepository.SaveChanges();
            if(result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Hr")]
        public ActionResult Delete (int id)
        {
            var employee = _employeeRepository.GetDetails(id);
            if(employee == null)
            {
                return NotFound();
            }
            else
            {
                _employeeRepository.Delete(employee);
                _employeeRepository.SaveChanges();

                return NoContent();
            }
        }
    }
}

//eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoibWljaGVsbGVzY290QHRlc3QuY29tIiwicm9sZSI6IkVtcGxveWVlIiwibmJmIjoxNjg5OTQwMTU5LCJleHAiOjE2OTA1NDQ5NTksImlhdCI6MTY4OTk0MDE1OX0.uZAuawM93NcGSEWBwcvskw91Bv26AirswPK96lKIOrY5yQAIbH2mwEJRaWrcmxYF - m3usajXODvrfNhA6YeV5w