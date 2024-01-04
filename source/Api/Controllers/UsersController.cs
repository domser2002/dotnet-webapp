using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Frontend.Validators.Abstractions;
using Frontend.Validators;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

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

        // GET api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<List<User>> GetByID(string id)
        {
            var users = repository.GetAll();
            foreach (User user in users) if (user.Auth0Id == id) return Ok(user);
            return BadRequest($"User with id {id} does not exist in the database.");
        }

        // Must have Microsoft.AspNetCore.Mvc.NewtonsoftJson installed
        // PATCH /api/users/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchByID(string id, [FromBody] JsonPatchDocument<User> patchEntity)
        {
            var users = repository.GetAll();
            var entity = users.FirstOrDefault(user => user.Auth0Id == id);
            if (entity == null) return BadRequest();
            patchEntity.ApplyTo(entity, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            return Ok(entity);
        }
        // GET api/users/count
        [HttpGet("count")]
        public ActionResult<int> GetUserCount()
        {
            var users = repository.GetAll();

            int count = users.Count();


            return Ok(count);
        }
        // POST api/users (dodawanie nowego u�ytkownika)
        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            ValidationResults validationResults = validator.Validate(user);
            if (validationResults == null)
                return BadRequest("could not validate");
            if (!validationResults.Success && validationResults.Message != "Enter a proper email address.")
                return BadRequest(validationResults.Message);

            repository.AddUser(user);
            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
    }
}