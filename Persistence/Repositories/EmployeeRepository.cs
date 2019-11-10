using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Models.Queries;
using ActivityAcme.API.Domain.Repositories;
using ActivityAcme.API.Persistence.Contexts;

namespace ActivityAcme.API.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public async Task<QueryResult<Employee>> ListAsync(EmployeesQuery query)
        {
            IQueryable<Employee> queryable = _context.Employees
                                                    .Include(p => p.Activity)
                                                    .AsNoTracking(); 
                                    
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if(query.ActivityId.HasValue && query.ActivityId > 0)
            {
                queryable = queryable.Where(p => p.ActivityId == query.ActivityId);
            }
            
            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();
            
            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<Employee> employees = await queryable.Skip((query.Page - query.ItemsPerPage) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client calculations of pages).
            return new QueryResult<Employee>
            {
                Items = employees,
                TotalItems = totalItems,
            };
        }

        public async Task<Employee> FindByIdAsync(int id)
        {
            return await _context.Employees
                                 .Include(p => p.Activity)
                                 .FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method return, we can't use FindAsync
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
        }
    }
}