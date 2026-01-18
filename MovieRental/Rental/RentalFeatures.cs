using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MovieRental.Customer;
using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Notification;
using MovieRental.Payment;
using MovieRental.Payment.DTOs;
using MovieRental.Shared;

namespace MovieRental.Rental
{
    public class RentalFeatures : BaseFeature, IRentalFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        private readonly IPaymentService _paymentService;
        private readonly ICustomerFeatures _customerFeatures;
        private readonly IMovieFeatures _movieFeatures;

        public RentalFeatures(
            MovieRentalDbContext movieRentalDb,
            IPaymentService paymentService,
            ICustomerFeatures customerFeatures,
            IMovieFeatures movieFeatures,
            INotifier notifier) : base(notifier)
        {
            _movieRentalDb = movieRentalDb;
            _paymentService = paymentService;
            _customerFeatures = customerFeatures;
            _movieFeatures = movieFeatures;
        }

        public async Task<Rental> Save(Rental rental)
        {
            if (!ExecuteValidation(new RentalValidation(), rental) 
                // || !ExecuteValidation(new CustomerValidation(), rental.Customer) 
                // || !ExecuteValidation(new MovieValidation(), rental.Movie)
               ) return null;

            using var transaction = await _movieRentalDb.Database.BeginTransactionAsync();
            try
            {
                PaymentMethod paymentMethod = Enum.Parse<PaymentMethod>(rental.PaymentMethod);
                PaymentRequest paymentRequest = new PaymentRequest(
                    Amount: rental.Amount,
                    PaymentMethod: paymentMethod
                );
                PaymentMetadata paymentMetadata = await _paymentService.ProcessPayAsync(paymentRequest);
                if (paymentMetadata != null && !paymentMetadata.Success)
                {
                    Notify("Payment Failed");
                    return null;
                }
                
                Movie.Movie movie = await _movieFeatures.Save(rental.Movie);
                Customer.Customer customer = await _customerFeatures.Save(rental.Customer);
                if (customer == null || movie == null)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
                
                var rentalToSave = new Rental()
                {
                    DaysRented = rental.DaysRented,
                    Amount = rental.Amount,
                    MovieId = movie.Id,
                    PaymentMethod = rental.PaymentMethod,
                    CustomerId = customer.Id
                };
                
                _movieRentalDb.Rentals.Add(rentalToSave);
                await _movieRentalDb.SaveChangesAsync();
                await transaction.CommitAsync();
                return rentalToSave;
            }
            catch (Exception e)
            {
               await transaction.RollbackAsync();
               throw;
            }
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