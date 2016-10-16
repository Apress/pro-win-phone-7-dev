using System.Collections.Generic;

namespace SampleData.LiveData
{
  public class Customers
  {
    private List<Customer> _customerList;

    public Customers()
    {
      _customerList = new List<Customer>()
      {
        new Customer(){FirstName="Rob",LastName="Cameron",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Atlanta",State="GA", Zip="30042"},
        new Customer(){FirstName="Amanda",LastName="Cam",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Philadephia",State="PA", Zip="19111"},
        new Customer(){FirstName="Anna",LastName="Ron",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="New York",State="NY", Zip="10001"},
        new Customer(){FirstName="John",LastName="Smith",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Chicago",State="IL", Zip="20011"},
        new Customer(){FirstName="Jane",LastName="Doe",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="San Francisco",State="CA", Zip="30333"},
        new Customer(){FirstName="Daniel",LastName="Booth",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Dallas",State="TX", Zip="79999"},
        new Customer(){FirstName="Arthur",LastName="Olson",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Seattle",State="WA", Zip="50000"},
        new Customer(){FirstName="Janet",LastName="Rich",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Portland",State="OR", Zip="43334"},
        new Customer(){FirstName="Janus",LastName="Poor",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Sacramento",State="CA", Zip="85755"},
        new Customer(){FirstName="Alice",LastName="Mer",PhoneNumber="555-555-5555",
                        Address="123 Main Street", City="Kansas City",State="KA", Zip="48488"}
      };
    }
    public List<Customer> CustomerList { get { return _customerList; } }
  }
}
