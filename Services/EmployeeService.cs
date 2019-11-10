using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Domain.Repositories;
using ActivityAcme.API.Domain.Services;
using ActivityAcme.API.Domain.Services.Communication;
using ActivityAcme.API.Infrastructure;

namespace ActivityAcme.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IMemoryCache _cache;

        public EmployeeService(IEmployeeRepository employeeRepository, IActivityRepository activityRepository, IMemoryCache cache)
        {
            _employeeRepository = employeeRepository;
            _activityRepository = activityRepository;
            _cache = cache;
        }

        public async Task<QueryResult<Employee>> ListAsync(EmployeesQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForEmployeesQuery(query);
            
            var employee = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _employeeRepository.ListAsync(query);
            });

            return employee;
        }

        public async Task<EmployeeResponse> SaveAsync(Employee employee)
        {
            try
            {
                /*
                 Notice here we have to check if the activity ID is valid before adding the employee, to avoid errors.
                 You can create a method into the ActivityService class to return the activity and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingActivity = await _activityRepository.FindByIdAsync(employee.ActivityId);
                if (existingActivity == null)
                    return new EmployeeResponse("Invalid activity.");

                await _employeeRepository.AddAsync(employee);
                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            var existingActivity = await _activityRepository.FindByIdAsync(employee.ActivityId);
            if (existingActivity == null)
                return new EmployeeResponse("Invalid activity.");

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Comments = employee.Comments;
            existingEmployee.ActivityId = employee.ActivityId;

            try
            {
                _employeeRepository.Update(existingEmployee);
                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            try
            {
                _employeeRepository.Remove(existingEmployee);
                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
            }
        }

        private string GetCacheKeyForEmployeesQuery(EmployeesQuery query)
        {
            string key = CacheKeys.EmployeesList.ToString();
            
            if (query.ActivityId.HasValue && query.ActivityId > 0)
            {
                key = string.Concat(key, "_", query.ActivityId.Value);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }
    }
}