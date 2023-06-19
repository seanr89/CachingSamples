using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // public string Contact { get; set; }
    // public string Email { get; set; }
    // public DateTime DateOfBirth { get; set; }

    public Customer(int id, string firstname, string lastname)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
    }
}