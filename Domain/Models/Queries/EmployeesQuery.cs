namespace ActivityAcme.API.Domain.Models.Queries
{
    public class EmployeesQuery : Query
    {
        public int? ActivityId { get; set; }

        public EmployeesQuery(int? activityId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            ActivityId = activityId;
        }
    }
}