using MovieRental.Payment.DTOs;
using MovieRental.PaymentProviders;

namespace MovieRental.Payment;

public class PaymentService : IPaymentService
{
    private readonly IEnumerable<IPaymentProvider> _paymentProviders;

    public PaymentService(IEnumerable<IPaymentProvider> paymentProviders)
    {
        _paymentProviders = paymentProviders;
    }

    public async Task<PaymentMetadata> ProcessPayAsync(PaymentRequest paymentRequest)
    {
        IPaymentProvider provider =
            _paymentProviders.FirstOrDefault(p => p.PaymentMethod.Equals(paymentRequest.PaymentMethod));

        if (provider is null)
        {
            return new PaymentMetadata(
                Success: false,
                Message: "Payment Provider not found"
            );
        }

        try
        {
            bool isSucess = await provider.Pay(paymentRequest.Amount);
            return new PaymentMetadata(
                Success: isSucess,
                Message: "Payment Success"
            );
        }
        catch (Exception e)
        {
            return new PaymentMetadata(
                Success: false,
                Message: "Payment Failed"
            );
        }
    }
}