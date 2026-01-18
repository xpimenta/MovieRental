namespace MovieRental.Payment.DTOs;

public record PaymentMetadata(
    bool Success,
    string Message
);