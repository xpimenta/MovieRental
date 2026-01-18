using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Notification;
using MovieRental.Shared;

namespace MovieRental.Customer;

public class CustomerFeatures : BaseFeature,  ICustomerFeatures
{
    private readonly MovieRentalDbContext _movieRentalDb;
    public CustomerFeatures(MovieRentalDbContext  movieRentalDb, INotifier _notifier) :  base(_notifier)
    {
        _movieRentalDb = movieRentalDb;
    }
    
    public async Task<Customer> Save(Customer customer)
    {
        if (!ExecuteValidation(new CustomerValidation(), customer)) return customer;
        
        if(await _movieRentalDb.Customers.AnyAsync(m => m.Name == customer.Name))
        {
            Notify("Customer already exists");
            return null;
        }
 
        _movieRentalDb.Customers.Add(customer);
        await _movieRentalDb.SaveChangesAsync();
        return customer;
    }

    public List<Customer> GetAll()
    {
        return _movieRentalDb.Customers.ToList();
    }
}