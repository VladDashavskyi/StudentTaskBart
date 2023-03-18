using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskBart.Services;
using StudentTaskBart;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly MyDbContext _dbContext;
    
    public IncidentController(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult CreateIncident(CreateIncidentRequest request)
    {
        var account = _dbContext.Accounts.SingleOrDefault(a => a.Name == request.AccountName);
        if (account == null)
        {
            return BadRequest("Account not found");
        }

        var contact = account.Contacts.SingleOrDefault(c => c.Email == request.ContactEmail);
        if (contact == null)
        {
            return BadRequest("Contact not found");
        }

        var incident = new Incident { };

        _dbContext.Incidents.Add(incident);
        _dbContext.SaveChanges();

        return Ok(incident);
    }
    
    [HttpGet]
    public IActionResult GetIncidents()
    {
        var incidents = _dbContext.Incidents.Include(i => i.Account).ThenInclude(a => a.Contacts).ToList();

        return Ok(incidents);
    }
}

public class CreateIncidentRequest
{
    public string AccountName { get; set; }
    public string ContactEmail { get; set; }
    public string IncidentDescription { get; set; }
}

