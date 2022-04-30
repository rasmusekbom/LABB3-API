using Rasmus_labb3_API.Contexts;
using Rasmus_labb3_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Repositories
{
    public class InterestRepo : IRepository<Interest, int>
    {
        private ApiDbContext _context;
        public InterestRepo(ApiDbContext context)
        {
            _context = context;
        }
        //Delete a specifiec Interest by ID
        public async Task<Interest> Delete(int id)
        {
            var interest = await _context.Interests.FirstOrDefaultAsync(interest => interest.InterestId == id);
            if(interest != null)
            {
                _context.Interests.Remove(interest);
                return interest;
            }
            return null;
        }

        //Get all the interests
        public async Task<IEnumerable<Interest>> GetAll()
        {
            return await _context.Interests
                .Include(i => i.Links)
                .Include(pi => pi.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .Include(l => l.PersonInterests)
                .ThenInclude(pi => pi.Interest)
                .ThenInclude(i => i.Links)
                .ToListAsync();
        }

        //Get interest by InterestId
        public async Task<Interest> GetById(int id)
        {
            return await _context.Interests
                .Include(i => i.Links)
                .Include(i => i.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .Include(i => i.PersonInterests)
                .ThenInclude(pi => pi.Interest)
                .FirstOrDefaultAsync(interest => interest.InterestId == id);
        }

        //Insert new Interest
        public async Task<Interest> Insert(Interest entity)
        {
            var result = await _context.Interests.AddAsync(entity);
            return result.Entity;
        }
        
        //Save new changes.
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
