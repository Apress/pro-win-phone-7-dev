using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace CallingRemoteServices.pages
{
  public partial class WebServicePage : PhoneApplicationPage
  {
    public WebServicePage()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      AdventureWorksClient adventureWorksClient = new AdventureWorksClient();
      adventureWorksClient.FullProductListCompleted += 
        new EventHandler<FullProductListCompletedEventArgs>(
          adventureWorksClient_FullProductListCompleted);
      adventureWorksClient.FullProductListAsync();
    }

    void adventureWorksClient_FullProductListCompleted(
      object sender, FullProductListCompletedEventArgs e)
    {
      ProductsListBox.ItemsSource = e.Result;
    }
  }
}