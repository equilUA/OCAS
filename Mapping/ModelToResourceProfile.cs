using AutoMapper;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Extensions;
using ActivityAcme.API.Resources;

namespace ActivityAcme.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Activity, ActivityResource>();

            CreateMap<Employee, EmployeeResource>();

            CreateMap<QueryResult<Employee>, QueryResultResource<Employee>>();
        }
    }
}