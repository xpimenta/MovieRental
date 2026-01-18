namespace MovieRental.Movie;

public interface IMovieFeatures
{
	Task<Movie> Save(Movie movie);
	List<Movie> GetAll();
}