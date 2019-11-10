using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAcme.API.Domain.Models;

namespace ActivityAcme.API.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> ListAsync();
        Task AddAsync(Activity category);
        Task<Activity> FindByIdAsync(int id);
        void Update(Activity category);
        void Remove(Activity category);
    }
}