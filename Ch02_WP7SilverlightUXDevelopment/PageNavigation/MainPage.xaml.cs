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
using System.Windows.Navigation;

namespace PageNavigation
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    
    private void NavToPage2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.NavigationService.Navigate(new Uri(String.Format("/Page2.xaml?page2data={0}",textBox1.Text), UriKind.Relative));
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigating += new NavigatingCancelEventHandler(NavigationService_Navigating);
      NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
    } 

    void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
    {
      progressBar1.Visibility = Visibility.Visible;
      progressBar1.IsIndeterminate = true;
    }

    void NavigationService_Navigated(object sender, NavigationEventArgs e)
    {
      progressBar1.Visibility = Visibility.Collapsed;
      progressBar1.IsIndeterminate = false;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      base.OnNavigatedFrom(e);
    }
  }
}