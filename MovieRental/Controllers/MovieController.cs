using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Notification;

namespace MovieRental.Controllers
{
    [Route("[controller]")]
    public class MovieController : MainController
    {
        private readonly IMovieFeatures _features;
        
        public MovieController(IMovieFeatures features, INotifier notifier) : base(notifier)
        {
            _features = features;
        }

        [HttpGet]
        public IActionResult Get()
        {
	        return Ok(_features.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie.Movie movie)
        {
            Movie.Movie saveMovie = await _features.Save(movie);
            if (saveMovie == null)
            {
                return CustomResponse();
            }
            return CustomResponse(HttpStatusCode.Created, saveMovie);
        }
    }
}
