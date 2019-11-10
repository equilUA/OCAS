using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Domain.Services;
using ActivityAcme.API.Resources;

namespace ActivityAcme.API.Controllers
{
    [Route("/api/employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all existing employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<EmployeeResource>), 200)]
        public async Task<QueryResultResource<EmployeeResource>> ListAsync([FromQuery] EmployeesQueryResource query)
        {
            var employeesQuery = _mapper.Map<EmployeesQueryResource, EmployeesQuery>(query);
            var queryResult = await _employeeService.ListAsync(employeesQuery);

            var resource = _mapper.Map<QueryResult<Employee>, QueryResultResource<EmployeeResource>>(queryResult);
            return resource;
        }

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="resource">Employee data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
        {
            var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);
            var result = await _employeeService.SaveAsync(employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);
            return Ok(employeeResource);
        }

        /// <summary>
        /// Updates an existing employee according to an identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <param name="resource">Employee data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
        {
            var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);
            var result = await _employeeService.UpdateAsync(id, employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);
            return Ok(employeeResource);
        }

        /// <summary>
        /// Deletes a given employee according to an identifier.
        /// </summary>
        /// <param name="id">Employee identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EmployeeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var activityResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);
            return Ok(activityResource);
        }
    }
}