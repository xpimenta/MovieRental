using FluentValidation;
using MovieRental.Configurations;
using MovieRental.Customer;
using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Notification;
using MovieRental.Payment;
using MovieRental.PaymentProviders;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IMovieFeatures, MovieFeatures>();
builder.Services.AddScoped<ICustomerFeatures, CustomerFeatures>();

builder.Services.AddPaymentScoped();

builder.Services.AddScoped<INotifier, Notifier>();


builder.Services.AddValidatorsFromAssemblyContaining<MovieValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<RentalValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
