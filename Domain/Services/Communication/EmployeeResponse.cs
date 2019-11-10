using ActivityAcme.API.Domain.Models;

namespace ActivityAcme.API.Domain.Services.Communication
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        public EmployeeResponse(Employee employee) : base(employee) { }

        public EmployeeResponse(string message) : base(message) { }
    }
}