public class Account
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<Contact> Contacts { get; set; }
    public List<Incident> Incidents { get; set; }

}