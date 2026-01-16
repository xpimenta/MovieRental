using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
    public class RentalFeatures : IRentalFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;

        public RentalFeatures(MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }

        public async Task<Rental> Save(Rental rental)
        {
            _movieRentalDb.Rentals.Add(rental);
            await _movieRentalDb.SaveChangesAsync();
            return rental;
        }

        public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
        {
            // Or Activate lazy loading + virtual
            return _movieRentalDb.Rentals
                .Include(c => c.Customer)
                .Include(m => m.Movie)
                .Where(c => c.Customer.Name == customerName)
                .ToList();
        }
    }
}