using MovieRental.Payment;
namespace MovieRental.Payment.DTOs;

public record PaymentRequest(
    double Amount,
    PaymentMethod PaymentMethod
);