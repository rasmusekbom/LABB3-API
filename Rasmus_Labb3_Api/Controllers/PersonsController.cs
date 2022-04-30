using Rasmus_labb3_API.Repositories;
using Rasmus_labb3_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rasmus_labb3_API.Utilities;

namespace Rasmus_labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepo;
        private readonly IRepository<Interest, int> _interestRepo;
        private readonly IRepository<Link, int> _linkRepo;
        private readonly IRepository<PersonInterest, int> _personInterestRepo;

        public PersonsController(IRepository<Person, int> personRepo,
            IRepository<Interest, int> interestRepo, 
            IRepository<Link, int> linkRepo, 
            IRepository<PersonInterest, int> personInterestRepo)
        {
            _personRepo = personRepo;
            _interestRepo = interestRepo;
            _linkRepo = linkRepo;
            _personInterestRepo = personInterestRepo;
        }

        //Get all persons
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                var persons = await _personRepo.GetAll();
                if (persons.ToList().Count != 0)
                {
                    return Ok(persons);
                }
                else
                {
                    return NotFound("No persons in the DB!");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again!");
            }

        }

        //Get person by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            try
            {
                var person = await _personRepo.GetById(id);
                if (person != null)
                {
                    return Ok(person);
                }
                else
                {
                    return NotFound($"{id}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }
    }
}
