﻿using Rasmus_labb3_API.Contexts;
using Rasmus_labb3_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Repositories
{
    public class PersonInterestRepo : IRepository<PersonInterest, int>
    {
        private ApiDbContext _context;
        public PersonInterestRepo(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<PersonInterest> Delete(int id)
        {
            var personInterest = await _context.PersonInterests.FirstOrDefaultAsync(pi => pi.PersonInterestId == id);

            if (personInterest != null)
            {
                _context.PersonInterests.Remove(personInterest);
                return personInterest;
            }
            return null;
        }

        public async Task<IEnumerable<PersonInterest>> GetAll()
        {
            return await _context.PersonInterests.Include(pi => pi.Interest)
                .ThenInclude(i => i.Links)
                .Include(pi => pi.Interest)
                .ThenInclude(i => i.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .Include(pi => pi.Person)
                .ToListAsync();
        }

        public async Task<PersonInterest> GetById(int id)
        {
            return await _context.PersonInterests.Include(pi => pi.Interest)
                .ThenInclude(i => i.Links)
                .Include(pi => pi.Interest)
                .ThenInclude(i => i.PersonInterests)
                .ThenInclude(pi => pi.Person)
                .Include(pi => pi.Person)
                .FirstOrDefaultAsync(pi => pi.PersonInterestId== id);
        }

        public async Task<PersonInterest> Insert(PersonInterest entity)
        {
            var result = await _context.PersonInterests.AddAsync(entity);
            return result.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
