using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;

namespace Api.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
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

        // POST api/users (dodawanie nowego użytkownika)
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