using MovieRental.Payment;
namespace MovieRental.PaymentProviders;

public interface IPaymentProvider
{
    public Task<bool> Pay(double price);
    PaymentMethod PaymentMethod { get; }
}