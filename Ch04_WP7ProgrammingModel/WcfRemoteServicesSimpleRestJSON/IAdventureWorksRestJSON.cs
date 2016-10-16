using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfRemoteServicesSimpleRestJSON.Models;

namespace WcfRemoteServicesSimpleRestJSON
{
  [ServiceContract]
  public interface IAdventureWorksRestJSON
  {
    [OperationContract]
    [WebGet(UriTemplate = "/Vendors", 
      BodyStyle = WebMessageBodyStyle.Bare, 
      ResponseFormat = WebMessageFormat.Json)]
    ObservableCollection<Vendor> GetVendors();

  }
}

