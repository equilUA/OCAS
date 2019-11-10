using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;

namespace ActivityAcme.API.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<QueryResult<Employee>> ListAsync(EmployeesQuery query);
        Task AddAsync(Employee product);
        Task<Employee> FindByIdAsync(int id);
        void Update(Employee product);
        void Remove(Employee product);
    }
}