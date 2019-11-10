using AutoMapper;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Resources;

namespace ActivityAcme.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveActivityResource, Activity>();

            CreateMap<SaveEmployeeResource, Employee>();

            CreateMap<EmployeesQueryResource, EmployeesQuery>();
        }
    }
}