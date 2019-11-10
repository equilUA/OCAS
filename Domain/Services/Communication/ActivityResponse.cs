using ActivityAcme.API.Domain.Models;

namespace ActivityAcme.API.Domain.Services.Communication
{
    public class ActivityResponse : BaseResponse<Activity>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="activity">Saved activity.</param>
        /// <returns>Response.</returns>
        public ActivityResponse(Activity activity) : base(activity)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ActivityResponse(string message) : base(message)
        { }
    }
}