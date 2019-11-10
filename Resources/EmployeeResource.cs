namespace ActivityAcme.API.Resources
{
    public class EmployeeResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public ActivityResource Activity {get;set;}
    }
}