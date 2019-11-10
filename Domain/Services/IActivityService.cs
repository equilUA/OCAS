using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Services.Communication;

namespace ActivityAcme.API.Domain.Services
{
    public interface IActivityService
    {
         Task<IEnumerable<Activity>> ListAsync();
         Task<ActivityResponse> SaveAsync(Models.Activity category);
         Task<ActivityResponse> UpdateAsync(int id, Models.Activity category);
         Task<ActivityResponse> DeleteAsync(int id);
    }
}