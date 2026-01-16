using Microsoft.AspNetCore.Mvc;
using MovieRental.Customer;

namespace MovieRental.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerFeatures  _features;
    public CustomerController(ICustomerFeatures  features)
    {
        _features =  features;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_features.GetAll());
    }

    [HttpPost()]
    public ActionResult Post([FromBody] Customer.Customer customer)
    {
        return Ok(_features.Save(customer));
    }
}