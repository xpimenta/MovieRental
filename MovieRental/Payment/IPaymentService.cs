using MovieRental.Payment.DTOs;

namespace MovieRental.Payment;

public interface IPaymentService
{
    Task<PaymentMetadata> ProcessPayAsync(PaymentRequest paymentRequest);
}