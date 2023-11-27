using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Frontend.Validators.Abstractions;
using Frontend.Validators;

namespace Api.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IRegistrationValidator validator;

        public UsersController(IUserRepository repository,IRegistrationValidator validator)
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
        public ActionResult<List<User>> GetByID(int id)
        {
            var users = repository.GetAll();
            
            if(id > users.Count)
            {
                return BadRequest();
            }

            return Ok(users[id]);
        }

        // POST api/users (dodawanie nowego u¿ytkownika)
        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            
            repository.AddUser(user);

            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
    }

}