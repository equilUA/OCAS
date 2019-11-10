using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Services;
using ActivityAcme.API.Resources;

namespace ActivityAcme.API.Controllers
{
    [Route("/api/activitys")]
    [Produces("application/json")]
    [ApiController]
    public class ActivitysController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivitysController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all Activitys.
        /// </summary>
        /// <returns>List os Activitys.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityResource>), 200)]
        public async Task<IEnumerable<ActivityResource>> ListAsync()
        {
            var activitys = await _activityService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(activitys);

            return resources;
        }

        /// <summary>
        /// Saves a new activity.
        /// </summary>
        /// <param name="resource">Activity data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ActivityResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveActivityResource resource)
        {
            var activity = _mapper.Map<SaveActivityResource, Activity>(resource);
            var result = await _activityService.SaveAsync(activity);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);
            return Ok(activityResource);
        }

        /// <summary>
        /// Updates an existing activity according to an identifier.
        /// </summary>
        /// <param name="id">Activity identifier.</param>
        /// <param name="resource">Updated Activity data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActivityResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveActivityResource resource)
        {
            var activity = _mapper.Map<SaveActivityResource, Activity>(resource);
            var result = await _activityService.UpdateAsync(id, activity);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);
            return Ok(activityResource);
        }

        /// <summary>
        /// Deletes a given activity according to an identifier.
        /// </summary>
        /// <param name="id">Activity identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ActivityResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _activityService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);
            return Ok(activityResource);
        }
    }
}