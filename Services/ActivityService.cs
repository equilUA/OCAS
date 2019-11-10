using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Repositories;
using ActivityAcme.API.Domain.Services;
using ActivityAcme.API.Domain.Services.Communication;
using ActivityAcme.API.Infrastructure;

namespace ActivityAcme.API.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMemoryCache _cache;

        public ActivityService(IActivityRepository activityRepository, IMemoryCache cache)
        {
            _activityRepository = activityRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            // Here I try to get the activity list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the activitys from the repository.
            var activitys = await _cache.GetOrCreateAsync(CacheKeys.ActivitysList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _activityRepository.ListAsync();
            });
            
            return activitys;
        }

        public async Task<ActivityResponse> SaveAsync(Activity activity)
        {
            try
            {
                await _activityRepository.AddAsync(activity);
                return new ActivityResponse(activity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActivityResponse($"An error occurred when saving the activity: {ex.Message}");
            }
        }

        public async Task<ActivityResponse> UpdateAsync(int id, Activity activity)
        {
            var existingActivity = await _activityRepository.FindByIdAsync(id);

            if (existingActivity == null)
                return new ActivityResponse("ActivityAcme not found.");

            existingActivity.Name = activity.Name;

            try
            {
                _activityRepository.Update(existingActivity);
                return new ActivityResponse(existingActivity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActivityResponse($"An error occurred when updating the activity: {ex.Message}");
            }
        }

        public async Task<ActivityResponse> DeleteAsync(int id)
        {
            var existingActivity = await _activityRepository.FindByIdAsync(id);

            if (existingActivity == null)
                return new ActivityResponse("Activity not found.");

            try
            {
                _activityRepository.Remove(existingActivity);
                return new ActivityResponse(existingActivity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActivityResponse($"An error occurred when deleting the activity: {ex.Message}");
            }
        }
    }
}