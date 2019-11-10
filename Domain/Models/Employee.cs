namespace ActivityAcme.API.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

    }
}