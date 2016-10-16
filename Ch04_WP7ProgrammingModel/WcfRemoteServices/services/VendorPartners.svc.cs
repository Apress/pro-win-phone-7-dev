using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfRemoteServices.model;
using WcfRemoteServices.BingMapsGeoCodeSvc;
using System.Collections.ObjectModel;

namespace WcfRemoteServices.services
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VendorPartners" in code, svc and config file together.
  public class VendorPartners : IVendorPartners
  {
    public List<Vendor> FullVendorList()
    {
      AdventureWorksEntities entities = new AdventureWorksEntities();
      var products = from v in entities.Vendors select v;
      return products.ToList<Vendor>();
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
        ApplicationId = "AhZkLXRfdSEi_XRkUKCmjBaDsIvZf2baS-9jYy1HGPaGqJErHONhnk80jJdlmOLj"
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
      return location ;
    }
  }
}
