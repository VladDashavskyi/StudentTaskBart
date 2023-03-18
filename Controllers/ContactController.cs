using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace StudentTaskBart;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;

    public ContactsController(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
    {
        return await _context.Contacts.ToListAsync();
    }


    [HttpPost]
    public IActionResult CreateContact(Contact model)
    {
        var contact = _mapper.Map<Contact>(model);

        _context.Contacts.Add(contact);
        _context.SaveChanges();

        return Ok(contact);
    }
}