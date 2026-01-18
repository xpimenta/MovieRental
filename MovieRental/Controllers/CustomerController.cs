using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Customer;
using MovieRental.Notification;

namespace MovieRental.Controllers;

[Route("[controller]")]
public class CustomerController : MainController
{
    private readonly ICustomerFeatures  _features;
    public CustomerController(ICustomerFeatures  features, INotifier notifier) : base(notifier)
    {
        _features =  features;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_features.GetAll());
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] Customer.Customer customer)
    {
        Customer.Customer saveCustomer = await _features.Save(customer);
        if (saveCustomer == null)
        {
            return CustomResponse();
        }
        return CustomResponse(HttpStatusCode.Created,  saveCustomer);
    }
}