using System;
using System.Data.Services.Client;
using System.Windows;
using AdventureWorksModelOdataClient;
using Microsoft.Phone.Controls;

namespace CallingRemoteServices.pages
{
  public partial class RestDataServicesODataPage : PhoneApplicationPage
  {
    public RestDataServicesODataPage()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      AdventureWorksEntities context =
        new AdventureWorksEntities(
          new Uri("http://localhost:9090/services/AdventureWorksRestOData.svc"));

      DataServiceCollection<Vendor> vendors = 
        new DataServiceCollection<Vendor>(context);
      //Add REST URL for collection
      VendorsListBox.ItemsSource = vendors;
      vendors.LoadAsync(new Uri("/Vendors", UriKind.Relative));
    }
  }
}