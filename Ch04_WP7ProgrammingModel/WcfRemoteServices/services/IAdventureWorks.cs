using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfRemoteServices.model;
using System.Runtime.Serialization;

namespace WcfRemoteServices.services
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the
  //interface name "IProducts" in both code and config file together.
  [ServiceContract]
  public interface IAdventureWorks
  {
    [OperationContract]
    List<Product> FullProductList();

    [OperationContract]
    List<ProductCategory> ProductCategoryList();

    [OperationContract]
    List<ProductSubcategory> ProductSubcategoryList();

    [OperationContract]
    List<Product> GetProductsByCategory(Int32 CategoryID);

    [OperationContract]
    List<Product> GetProductsBySubcategory(Int32 SubCategoryID);

    [OperationContract]
    int CheckInventory(Int32 ProductID);

    [OperationContract]
    List<Vendor> FullVendorList();

    VendorGeoLocation GetVendorAddress(Int32 VendorID);
  }

  [DataContract]
  public class VendorGeoLocation
  {
    [DataMember]
    public double Latitude { get; set; }

    [DataMember]
    public double Longitude { get; set; }
  }
}
