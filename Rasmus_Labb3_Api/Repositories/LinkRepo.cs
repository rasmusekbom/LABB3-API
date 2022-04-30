using Rasmus_labb3_API.Contexts;
using Rasmus_labb3_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Repositories
{
    public class LinkRepo : IRepository<Link, int>
    {
        private ApiDbContext _context;
        public LinkRepo(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Link> Delete(int id)
        {
            var link = await _context.Links.FirstOrDefaultAsync(link => link.LinkId == id);
            if(link != null)
            {
                _context.Links.Remove(link);
                return link;
            }
            return null;
        }

        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _context.Links.Include(l => l.Interest)
                .ThenInclude(l => l.Links)
                .Include(l => l.Interest)
                .ThenInclude(l => l.PersonInterests)
                .ThenInclude(l => l.Person)
                .ToListAsync();
        }

        public async Task<Link> GetById(int id)
        {
            return await _context.Links.Include(l => l.Interest)
                .ThenInclude(i => i.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .Include(l => l.Interest)
                .ThenInclude(i => i.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .FirstOrDefaultAsync(link => link.LinkId == id);
        }

        public async Task<Link> Insert(Link entity)
        {
            var result = await _context.Links.AddAsync(entity);
            return result.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
