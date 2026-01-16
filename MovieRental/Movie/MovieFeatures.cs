using MovieRental.Data;

namespace MovieRental.Movie
{
    public class MovieFeatures : IMovieFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        public MovieFeatures(MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }
		
        public Movie Save(Movie movie)
        {
            _movieRentalDb.Movies.Add(movie);
            _movieRentalDb.SaveChanges();
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
                .Skip((pageNumber - 1) * pageSize) // pula os registros das páginas anteriores
                .Take(pageSize)                     // pega apenas pageSize registros
                .ToList();
        }
    }
}