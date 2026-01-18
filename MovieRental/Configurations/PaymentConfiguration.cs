using MovieRental.Payment;
using MovieRental.PaymentProviders;

namespace MovieRental.Configurations;

public static class PaymentConfiguration
{
    public static IServiceCollection AddPaymentScoped(this IServiceCollection services)
    {
        services.AddScoped<IPaymentProvider, MbWayProvider>();
        services.AddScoped<IPaymentProvider, PayPalProvider>();

        services.AddScoped<IPaymentService, PaymentService>();
        
        return services;
    }
}