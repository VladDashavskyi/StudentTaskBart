using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskBart;
using System.Linq;

namespace StudentTaskBart
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public AccountController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateAccount(CreateAccountRequest request)
        {
            var contact = new Contact
            {
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail
            };

            var account = new Account
            {
                Name = request.AccountName,
                Contacts = new List<Contact> { contact }
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _dbContext.Accounts.Include(a => a.Contacts).ToList();

            return Ok(accounts);
        }
    }

    public class CreateAccountRequest
    {
        public string AccountName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }
}
