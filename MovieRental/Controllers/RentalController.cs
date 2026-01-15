using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }
        
        [HttpGet]
        public IActionResult GetRentalsByCustomerName(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
            {
                return BadRequest();
            }
            return Ok(_features.GetRentalsByCustomerName(customerName));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.Save(rental));
        }
	}
}
