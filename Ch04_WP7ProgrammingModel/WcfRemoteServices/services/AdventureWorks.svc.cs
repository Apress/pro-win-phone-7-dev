using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfRemoteServices.model;
using WcfRemoteServices.BingMapsGeoCodeSvc;

namespace WcfRemoteServices.services
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change
  //the class name "Products" in code, svc and config file together.
  public class AdventureWorks : IAdventureWorks
  {
    public List<Product> FullProductList()
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var products = from p in entities.Products select p;
      return products.ToList<Product>();
    }

    public List<ProductCategory> ProductCategoryList()
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var categories = from c in entities.ProductCategories select c;
      return categories.ToList<ProductCategory>();
    }

    public List<ProductSubcategory> ProductSubcategoryList()
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var categories = from sc in entities.ProductSubcategories select sc;
      return categories.ToList<ProductSubcategory>();
    }

    public List<Product> GetProductsByCategory(Int32 CategoryID)
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();

      var products = from p in entities.Products
                     where p.ProductSubcategoryID == CategoryID
                     select p;
      return products.ToList<Product>();
    }

    public List<Product> GetProductsBySubcategory(Int32 SubcategoryID)
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var products = from p in entities.Products
                     where p.ProductSubcategoryID == SubcategoryID
                     select p;
      return products.ToList<Product>();
    }

    public int CheckInventory(int ProductID)
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var inventory = from i in entities.ProductInventories
                      where i.ProductID == ProductID
                      select i.Quantity;

      return inventory.First<short>();
    }

    public List<Vendor> FullVendorList()
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var vendors = from v in entities.Vendors select v;
      return vendors.ToList<Vendor>();
    }

    public VendorGeoLocation GetVendorAddress(int VendorID)
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var vendorAddress = from va in entities.VendorAddresses
                          join
                            a in entities.Addresses
                            on va.AddressID equals a.AddressID
                          select a;

      model.Address address = vendorAddress.First<model.Address>();

      GeocodeServiceClient geoClient = new GeocodeServiceClient();
      GeocodeRequest request = new GeocodeRequest();
      request.Address = new BingMapsGeoCodeSvc.Address()
      {
        AddressLine = address.AddressLine1,
        PostalCode = address.PostalCode,
        PostalTown = address.City,
      };
      //Credentials to your AppID from the Bing Maps portal
      request.Credentials = new Credentials()
      {
        ApplicationId = 
        "AhZkLXRfdSEi_XRkUKCmjBaDsIvZf2baS-9jYy1HGPaGqJErHONhnk80jJdlmOLj"
      };
      //Set filter to high confidence
      request.Options = new GeocodeOptions()
      {
        Filters = new FilterBase[]
        {
            new ConfidenceFilter()
            {
                MinimumConfidence = Confidence.High
            }
        }
      };
      GeocodeResponse response = geoClient.Geocode(request);
      //Get the highest confidence result
      var vendorGeoLoc = (from r in response.Results

                          orderby (int)r.Confidence ascending

                          select r).FirstOrDefault();

      //take vendor address and look it up on Bing
      VendorGeoLocation location = new VendorGeoLocation()
      {
        Latitude = vendorGeoLoc.Locations[0].Latitude,
        Longitude = vendorGeoLoc.Locations[0].Longitude
      };
      return location;
    }
  }
}
