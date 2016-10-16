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
using System.Device.Location;
using System.Collections.ObjectModel;

namespace BingMapsControl
{
  public partial class MainPage : PhoneApplicationPage
  {
    
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void AddaPinAppBarBtn_Click(object sender, EventArgs e)
    {
      //SinglePushpin is defined in XAML
      GeoCoordinate location = new GeoCoordinate(34d, -84d);
      SinglePushpin.Location = location;
      SinglePushpin.Visibility = Visibility.Visible;

      //Center and Zoom in on point
      TestBingMapsControl.Center = location;
      TestBingMapsControl.ZoomLevel = 11;
      //Turn on zoom bar for emulator testing
      TestBingMapsControl.ZoomBarVisibility = Visibility.Visible;
    }

    private void DatabindAppBarBtn_Click(object sender, EventArgs e)
    {
      PushpinCollection collection = LayoutRoot.DataContext as PushpinCollection;
      if (collection != null)
        collection.InitializePointsCollection();
    }
  }


}