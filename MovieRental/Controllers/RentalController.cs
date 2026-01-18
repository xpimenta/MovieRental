using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Notification;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [Route("[controller]")]
    public class RentalController : MainController
    {
        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features, INotifier notifier) : base(notifier)
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
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental)
        {
            Rental.Rental saveRental = await _features.Save(rental);
            if (saveRental == null)
                return CustomResponse();
   
	        return CustomResponse(HttpStatusCode.Created, saveRental);
        }
	}
}
