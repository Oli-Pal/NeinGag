using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
        private readonly DataContext _context;
        public BugController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerEeror(){
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(){
            var thing = _context.Users.Find(-1);
            if (thing == null) return NotFound();

            return Ok(thing);
        } 


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
            return BadRequest("That was not a good request!");
        } 
    }
}