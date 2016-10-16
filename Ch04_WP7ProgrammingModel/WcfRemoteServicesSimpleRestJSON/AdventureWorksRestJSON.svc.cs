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
          Name="Frabrikam Bikes" },
        new Vendor(){AccountNumber="222222", CreditRating=40,
          Name="Contoso Sports" },
        new Vendor(){AccountNumber="333333", CreditRating=30,
          Name="Duwamish Surfing Gear" },
      };

      return vendors;
    }

  }
}