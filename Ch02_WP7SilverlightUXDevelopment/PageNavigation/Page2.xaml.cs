using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PageNavigation
{
  public partial class Page2 : PhoneApplicationPage
  {
    public Page2()
    {
      InitializeComponent();

      System.Threading.Thread.Sleep(5000);
    }

    protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
    {
     
      base.OnNavigatedFrom(e);
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      if (NavigationContext.QueryString.ContainsKey("page2data"))
        dataFromMainPageTextBlock.Text = NavigationContext.QueryString["page2data"];

      base.OnNavigatedTo(e);
    }

    private void navToPage3TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      NavigationService.Navigate(new Uri(String.Format("/Page3.xaml?page3data={0}",dataForPage3TextBox.Text), UriKind.Relative));

    }
  }
}