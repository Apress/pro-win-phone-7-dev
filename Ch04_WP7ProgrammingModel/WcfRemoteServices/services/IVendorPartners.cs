using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfRemoteServices.model;

namespace WcfRemoteServices.services
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVendorPartners" in both code and config file together.
  [ServiceContract]
  public interface IVendorPartners
  {
    [OperationContract]
    List<Vendor> FullVendorList();

    VendorGeoLocation GetVendorAddress(Int32 VendorID);
  }

  [DataContract]
  public class VendorGeoLocation
  {
    [DataMember]
    public double Latitude {get; set;}

    [DataMember]
    public double Longitude { get; set; }
  }
}
