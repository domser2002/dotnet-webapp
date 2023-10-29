using Domain.Abstractions;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        private readonly IOfferRepository repository;

        public OffersController(IOfferRepository repository)
        {
            this.repository = repository;
        }

        // GET api/offers
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            var users = repository.GetAll();

            return Ok(users);
        }

        // GET api/offers/{id}
        [HttpGet("{id}")]
        public ActionResult<List<User>> GetByID(int id)
        {
            var users = repository.GetAll();

            if (id > users.Count)
            {
                return BadRequest();
            }

            return Ok(users[id]);
        }

        // POST api/offers (dodawanie nowej oferty)
        [HttpPost]
        public ActionResult<User> Create([FromBody] Offer offer)
        {
            if (offer == null)
            {
                return BadRequest();
            }

            repository.AddOffer(offer);

            return CreatedAtAction("GetById", new { id = offer.Id }, offer);
        }
    }
}