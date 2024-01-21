using Domain.Abstractions;
using Domain.Model;
using Infrastructure.LectureRepositories;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<List<Offer>> Get()
        {
            var offers = repository.GetAll();

            return Ok(offers);
        }

        // GET api/offers/{id}
        [HttpGet("{id}")]
        public ActionResult<List<Offer>> GetByID(int id)
        {
            var offer = repository.GetByID(id);
            return Ok(offer);
        }
        // POST api/offers/inquiry
        [HttpPost("/inquiry")]
        public ActionResult<List<Offer>> GetByInquiry([FromBody] Inquiry inquiry)
        {
            var offers = repository.GetByInquiry(inquiry);
            var lecture_offers = LectureOfferRepository.GetByInquiry(inquiry).Result;
            offers.AddRange(lecture_offers);
            return Ok(lecture_offers);
        }
        // POST api/offers (dodawanie nowej oferty)
        [HttpPost]
        public ActionResult<Offer> Create([FromBody] Offer offer)
        {
            if (offer == null)
            {
                return BadRequest();
            }

            repository.AddOffer(offer);

            return CreatedAtAction("GetById", new { id = offer.Id }, offer);
        }

        // PATCH api/offers/{id}
        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<string> Deactivate([FromRoute] int id)
        {
            repository.Deactivate(id);
            //offer.Active = false;
            return Ok("okej");
        }
    }
}