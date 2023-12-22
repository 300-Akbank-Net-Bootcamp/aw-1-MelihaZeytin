using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using VbApi.DatabaseContext;
using VbApi.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly PersonContext _context;
    public StaffController(PersonContext context)
    {
        _context = context;
    }

[HttpPost("Staff")]
public async Task<IActionResult> Staff([FromBody] Staff value)
{
    if (string.IsNullOrEmpty(value.Name) || value.Name.Length < 10 || value.Name.Length > 250 || string.IsNullOrEmpty(value.Name))
    {        
        return BadRequest("Invalid Name.");
    }
    else if (string.IsNullOrEmpty(value.Email) || !value.Email.Contains("@"))
    {
        return BadRequest("Email address is not valid.");
    }
    else if (string.IsNullOrEmpty(value.Phone) || !AreAllDigits(value.Phone))
    {
        return BadRequest("Phone is not valid.");
    }
    else if (value.HourlySalary < 30 || value.HourlySalary > 400)
    {
        return BadRequest("Hourly salary does not fall within the allowed range.");
    }

    _context.Staff.Add(new Staff()
    {
        Id = 0,
        Name = value.Name,
        Email = value.Email,
        Phone = value.Phone,
        HourlySalary = value.HourlySalary
    });

    await _context.SaveChangesAsync();

    return Ok("Staff added successfully.");
}

    static bool AreAllDigits(string str)
    {
        return str.All(char.IsDigit);
    }



}