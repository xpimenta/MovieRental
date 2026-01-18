using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Notification;
using MovieRental.Shared;

namespace MovieRental.Movie
{
    public class MovieFeatures : BaseFeature, IMovieFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        public MovieFeatures(MovieRentalDbContext movieRentalDb, INotifier notifier) : base(notifier)
        {
            _movieRentalDb = movieRentalDb;
        }
		
        public async Task<Movie> Save(Movie movie)
        {
            if (!ExecuteValidation(new MovieValidation(), movie)) return null;
            
            if(await _movieRentalDb.Movies.AnyAsync(m => m.Title == movie.Title))
            {
                Notify("Movie already exists");
                return null;
            }
            
            _movieRentalDb.Movies.Add(movie);
            await _movieRentalDb.SaveChangesAsync();
            return movie;
        }

        // We Don't have a DTO, so sensitive data is exposed.
        // If you have too many records, it can be dangerous for memory -> Pagination or filters (category, active) are recommended 
        public List<Movie> GetAll()
        {
            return _movieRentalDb.Movies.ToList();
        }
		
        public List<Movie> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            return _movieRentalDb.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize) 
                .ToList();
        }
    }
}