using System.Collections.Generic;

namespace ActivityAcme.API.Domain.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; } = new List<Employee>();
    }
}