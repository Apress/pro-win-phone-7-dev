using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Linq;
using System.Collections.Generic;

namespace LongListSelectorSample.pages
{
  public partial class LongListSelectorPage1 : PhoneApplicationPage
  {
    public LongListSelectorPage1()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      //AdventureWorksClient adventureWorksClient = new AdventureWorksClient();
      //adventureWorksClient.FullProductListCompleted += 
      //  new EventHandler<FullProductListCompletedEventArgs>(
      //    adventureWorksClient_FullProductListCompleted);
      //adventureWorksClient.FullProductListAsync();
    }

    //void adventureWorksClient_FullProductListCompleted(
    //  object sender, FullProductListCompletedEventArgs e)
    //{
    //  ProductsLongListBox.ItemsSource = e.Result.ToList<Product>();
    //}
  }
}