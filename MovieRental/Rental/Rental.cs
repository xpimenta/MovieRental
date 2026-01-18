using System.ComponentModel.DataAnnotations.Schema;
using MovieRental.Shared;

namespace MovieRental.Rental
{
	public class Rental : Entity
	{
		public int DaysRented { get; set; }
		public int Amount { get; set; }
		public Movie.Movie? Movie { get; set; }

		[ForeignKey("Movie")]
		public int MovieId { get; set; }

		public string PaymentMethod { get; set; }

		// TODO: we should have a table for the customers

		public Customer.Customer? Customer { get; set; }
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		// public string CustomerName { get; set; }
	}
}
