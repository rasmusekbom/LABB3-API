using Rasmus_labb3_API.Repositories;
using Rasmus_labb3_API.Utilities;
using Rasmus_labb3_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        //DI-repos
        private readonly IRepository<Person, int> _personRepo;
        private readonly IRepository<Interest, int> _interestRepo;
        private readonly IRepository<Link, int> _linkRepo;
        private readonly IRepository<PersonInterest, int> _personInterestRepo;

        public InterestsController(IRepository<Person, int> personRepo,
            IRepository<Interest, int> interestRepo,
            IRepository<Link, int> linkRepo,
            IRepository<PersonInterest, int> personInterestRepo)
        {
            _personRepo = personRepo;
            _interestRepo = interestRepo;
            _linkRepo = linkRepo;
            _personInterestRepo = personInterestRepo;
        }

        //Get all interests
        [HttpGet]
        public async Task<IActionResult> GetAllInterests()
        {
            try
            {
                var interests = await _interestRepo.GetAll();

                if (interests.ToList().Count != 0)
                {
                    return Ok(interests);
                }
                else
                {
                    return NotFound("No interests in the DB.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }

        //Get interest by interestId.
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetInterestById(int id)
        {
            try
            {
                var result = await _interestRepo.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }

        //Get PersonInterest by PersonInterestId, example: api/interests/personinterestbyid:3
        [HttpGet("personinterestbyid:{id}")]
        public async Task<IActionResult> GetPersonInterestById(int id)
        {
            try
            {
                var result = await _personInterestRepo.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }

        //Get interests connected to a person by Personname filtered.
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllInterestFromSpecifiedPerson(string name)
        {
            try
            {
                var persons = await _personRepo.GetAll();
                var personFromName = persons.PersonByName(name);
                if (personFromName != null)
                {
                    var person = await _personRepo.GetById(personFromName.PersonId);
                    var interests = await _interestRepo.GetAll();
                    var query = interests.Join(await _personInterestRepo.GetAll(), o => o.InterestId, i => i.InterestId, (o, i) => new { o, i })
                        .Join(persons, o => o.i.PersonId, i => i.PersonId, (interest, person) => new { Person = person, Interest = interest.o, Links = interest.o.Links })
                        .Where(a => a.Person.PersonId == person.PersonId);
                    return Ok(query.Distinct());
                }
                else
                {
                    return NotFound($"No interests linked to {name} was found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }

        //Connect a person to a new interest
        [HttpPost]
        public async Task<ActionResult<PersonInterest>> CreatePersonInterest(PersonInterest personInterest)
        {
            try
            {
                var pi = await _personInterestRepo.GetAll(); 
                if(!(pi.Any(pi => pi.PersonId == personInterest.PersonId) && pi.Any(pi => pi.InterestId == personInterest.InterestId)))
                {
                    return BadRequest();
                }
                var createdPI = await _personInterestRepo.Insert(personInterest);
                await _personInterestRepo.Save();
                return CreatedAtAction(nameof(GetPersonInterestById), new { id =createdPI.PersonInterestId}, personInterest);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }

    }
}
