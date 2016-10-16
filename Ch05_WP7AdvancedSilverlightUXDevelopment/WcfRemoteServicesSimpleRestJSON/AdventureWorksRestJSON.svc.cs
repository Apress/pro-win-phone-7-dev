using WcfRemoteServicesSimpleRestJSON.Models;
using System.Collections.ObjectModel;

//TEST THE SERVICE: http://localhost:9191/AdventureWorksRestJSON.svc/Vendors

namespace WcfRemoteServicesSimpleRestJSON
{
  public class AdventureWorksRestJSON : IAdventureWorksRestJSON
  {

    public ObservableCollection<Vendor> GetVendors()
    {
      //Replace with real data layer here
      ObservableCollection<Vendor> vendors = new ObservableCollection<Vendor>()
      {
        new Vendor(){AccountNumber="111111", CreditRating=65,
          Name="Fabrikam Bikes" },
        new Vendor(){AccountNumber="222222", CreditRating=40,
          Name="Contoso Sports" },
        new Vendor(){AccountNumber="333333", CreditRating=30,
          Name="Duwamish Surfing Gear" },
        new Vendor(){AccountNumber="444444", CreditRating=65,
          Name="Contoso Bikes" },
        new Vendor(){AccountNumber="555555", CreditRating=40,
          Name="Fabrikam Sports" },
        new Vendor(){AccountNumber="666666", CreditRating=30,
          Name="Duwamish Golf" },
        new Vendor(){AccountNumber="777777", CreditRating=65,
          Name="Fabrikam Sun Sports" },
        new Vendor(){AccountNumber="888888", CreditRating=40,
          Name="Contoso Lacross" },
        new Vendor(){AccountNumber="999999", CreditRating=30,
          Name="Duwamish Team Sports" },
      };

      return vendors;
    }
  }
}