using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using VbApi.DatabaseContext;
using VbApi.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly PersonContext _context;
    public const int minJuniorSalary = 50;
    public const int minSeniorSalary = 200;
    public EmployeeController(PersonContext context)
    {
        _context = context;
    }


[HttpPost("Employee")]
public async Task<IActionResult> Employee([FromBody] Employee value)
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
    else if (value.HourlySalary < 50 || value.HourlySalary > 400)
    {
        return BadRequest("Hourly salary does not fall within allowed range.");
    }
    else
    {
        DateTime currentDate = DateTime.Now;
        int age = currentDate.Year - value.DateOfBirth.Year;

        if (!IsValidAge(age))
        {
            return BadRequest("Birthdate is not valid.");
        }
        else if ((age > 30 && value.HourlySalary < minSeniorSalary) || (age <= 30 && value.HourlySalary < minJuniorSalary))
        {
            return BadRequest("Minimum hourly salary is not valid.");
        }
    }

    _context.Employee.Add(new Employee()
    {
        Id = 0,
        Name = value.Name,
        DateOfBirth = value.DateOfBirth,
        Email = value.Email,
        Phone = value.Phone,
        HourlySalary = value.HourlySalary
    });

    await _context.SaveChangesAsync();

    return Ok("Employee added successfully.");
}


    static bool AreAllDigits(string str)
    {
        return str.All(char.IsDigit);
    }
    static bool IsValidAge(int age)
    {
        return age >= 0 && age <= 65;
    }
}