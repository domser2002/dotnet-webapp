using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Frontend.Validators.Abstractions;
using Frontend.Validators;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IRegistrationValidator validator;

        public UsersController(IUserRepository repository, IRegistrationValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            var users = repository.GetAll();

            return Ok(users);
        }

        // GET api/users/subs/{id}
        [HttpGet("subs/{id}")]
        public ActionResult<User> GetByID(string id)
        {
            var user = repository.GetById(id);
            if (user != null) return Ok(user);
            return BadRequest($"User with id {id} does not exist in the database.");
        }

        // Must have Microsoft.AspNetCore.Mvc.NewtonsoftJson installed
        // PATCH /api/users/subs/{id}
        [HttpPatch("/subs/{id}")]
        [Authorize]
        public ActionResult PatchByID(string id, [FromBody] UserPatchModel userPatch)
        {
            if (userPatch == null)
            {
                return BadRequest("Invalid request parameters");
            }
            User? existingUser = null;
            var users = repository.GetAll();
            foreach (User user in users) if (user.Auth0Id == id) existingUser = user;
            if (existingUser == null)
            {
                return NotFound("User not found");
            }
            if (!string.IsNullOrEmpty(userPatch.FirstName))
            {
                existingUser.FirstName = userPatch.FirstName;
            }
            if (!string.IsNullOrEmpty(userPatch.LastName))
            {
                existingUser.LastName = userPatch.LastName;
            }
            if(!string.IsNullOrEmpty(userPatch.Email))
            {
                existingUser.Email = userPatch.Email;
            }
            if(!string.IsNullOrEmpty(userPatch.CompanyName))
            {
                existingUser.CompanyName = userPatch.CompanyName;
            }
            if(userPatch.Address != null)
            {
                existingUser.Address = userPatch.Address;
            }
            if (userPatch.DefaultSourceAddress != null)
            {
                existingUser.DefaultSourceAddress = userPatch.DefaultSourceAddress;
            }
            ValidationResults validationResults = validator.Validate(existingUser);
            if (validationResults == null)
                return BadRequest("could not validate");
            if (!validationResults.Success)
                return BadRequest(validationResults.Message);
            repository.Update(existingUser);
            return Ok(existingUser);
        }
        // GET api/users/count
        [HttpGet("count")]
        public ActionResult<int> GetUserCount()
        {
            var users = repository.GetAll();
            int count = users.Count;
            return Ok(count);
        }
        // POST api/users (dodawanie nowego uzytkownika)
        [HttpPost]
        [Authorize]
        public ActionResult<User> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            ValidationResults validationResults = validator.Validate(user);
            if (validationResults == null)
                return BadRequest("could not validate");
            if (!validationResults.Success)
                return BadRequest(validationResults.Message);

            repository.AddUser(user);
            new Mailer.Mailer().SendRegistrationMail(user);
            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
    }
}