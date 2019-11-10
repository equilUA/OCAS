using System.Threading.Tasks;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Domain.Services.Communication;

namespace ActivityAcme.API.Domain.Services
{
    public interface IEmployeeService
    {
        Task<QueryResult<Employee>> ListAsync(EmployeesQuery query);
        Task<EmployeeResponse> SaveAsync(Employee product);
        Task<EmployeeResponse> UpdateAsync(int id, Employee product);
        Task<EmployeeResponse> DeleteAsync(int id);
    }
}