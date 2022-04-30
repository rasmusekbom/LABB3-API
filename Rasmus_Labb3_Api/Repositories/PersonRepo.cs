using Rasmus_labb3_API.Contexts;
using Rasmus_labb3_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Repositories
{
    public class PersonRepo : IRepository<Person, int>
    {
        private ApiDbContext _context;
        public PersonRepo(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Person> Delete(int id)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(person => person.PersonId == id);
            if(personToDelete != null)
            {
                _context.Persons.Remove(personToDelete);
                return personToDelete;
            }
            return null;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(person => person.PersonId == id);
        }

        public async Task<Person> Insert(Person entity)
        {
            var result = await _context.Persons.AddAsync(entity);
            return result.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
