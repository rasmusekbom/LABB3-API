using Rasmus_labb3_API.Repositories;
using Rasmus_labb3_API.Utilities;
using Rasmus_labb3_API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rasmus_labb3_Models;

namespace Rasmus_labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        //DI-repos
        private readonly IRepository<Person, int> _personRepo;
        private readonly IRepository<Interest, int> _interestRepo;
        private readonly IRepository<Link, int> _linkRepo;
        private readonly IRepository<PersonInterest, int> _personInterestRepo;

        public LinksController(IRepository<Person, int> personRepo,
            IRepository<Interest, int> interestRepo,
            IRepository<Link, int> linkRepo,
            IRepository<PersonInterest, int> personInterestRepo)
        {
            _personRepo = personRepo;
            _interestRepo = interestRepo;
            _linkRepo = linkRepo;
            _personInterestRepo = personInterestRepo;
        }

        //Get all Links
        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                var links = await _linkRepo.GetAll();
                if (links.ToList().Count != 0)
                {
                    return Ok(links);
                }
                else
                {
                    return NotFound("No links in database!");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error! Try again!");
            }
        }

        //Get links by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLinkById(int id)
        {
            try
            {
                var result = await _linkRepo.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error! Try again!");
            }
        }


        //Get all links by a specific Persons name
        [HttpGet("linksbyname:{name}")]
        public async Task<IActionResult> GetAllLinksFromSpecifiedPerson(string name)
        {
            try
            {
                var persons = await _personRepo.GetAll();
                var personFromName = persons.PersonByName(name);
                if (personFromName != null)
                {
                    var person = await _personRepo.GetById(personFromName.PersonId);
                    var links = await _linkRepo.GetAll();
                    var query = links.Join(await _personInterestRepo.GetAll(), o => o.InterestId, i => i.InterestId, (o, i) => new { o, i })
                        .Join(persons, o => o.i.PersonId, i => i.PersonId, (link, person) => new { Link = link.o, Person = person }).Where(p => p.Person.PersonId == person.PersonId);
                        
                    return Ok(query);
                }
                else
                {
                    return NotFound($"No links are connected to anyone named {name}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again!");
            }
        }

        //To create a new link to a Person and an Interest! 
        [HttpPost("{personName}/{interestName}")]
        public async Task<ActionResult<Link>> AddLinkForPersonAndInterests(string personName, string interestName, Link link)
        {
            try
            {
                if(link == null || string.IsNullOrEmpty(link.LinkUrl))
                {
                    return BadRequest();
                }

                var persons = await _personRepo.GetAll();
                var person = persons.PersonByName(personName);

                var interests = await _interestRepo.GetAll();
                var interest = interests.InterestByName(interestName);

                var personInterests = await _personInterestRepo.GetAll();

                if (person != null && interest != null)
                {
                    var personInterest = personInterests.FirstOrDefault(pi => pi.PersonId == person.PersonId && pi.InterestId == interest.InterestId);

                    if (personInterest != null)
                    {
                        link.Interest = interest;
                        var insertedLink = await _linkRepo.Insert(link);
                        await _linkRepo.Save();
                        return CreatedAtAction(nameof(GetLinkById), new { id=insertedLink.LinkId }, insertedLink);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound($"Can't find any {personName}, or interest {interestName} in the database.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error. Try again.");
            }
        }
    }
}
