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
using System.Windows.Threading;
using System.Globalization;

namespace SinglePointTouch
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      //Setup memory tracking timer
      DispatcherTimer TrackMemoryTimer = new DispatcherTimer();
      TrackMemoryTimer.Interval = new TimeSpan(0, 0, 0, 0, 5000);
      TrackMemoryTimer.Tick += DebugMemoryInfo_Tick;
      TrackMemoryTimer.Start();
    }

    // Track memory info
    void DebugMemoryInfo_Tick(object sender, EventArgs e)
    {
      //GC.GetTotalMemory(true);
      long deviceTotalMemory =
       (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
       "DeviceTotalMemory");
      long applicationCurrentMemoryUsage =
       (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
       "ApplicationCurrentMemoryUsage");
      long applicationPeakMemoryUsage =
       (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
       "ApplicationPeakMemoryUsage");

      System.Diagnostics.Debug.WriteLine("--> " +
        DateTime.Now.ToLongTimeString());
      System.Diagnostics.Debug.WriteLine("--> Device Total : " +
        deviceTotalMemory.ToString("#,#", CultureInfo.InvariantCulture));
      System.Diagnostics.Debug.WriteLine("--> App Current : " +
        applicationCurrentMemoryUsage.ToString("#,#", CultureInfo.InvariantCulture));
      System.Diagnostics.Debug.WriteLine("--> App Peak : " +
        applicationPeakMemoryUsage.ToString("#,#", CultureInfo.InvariantCulture));
    }
  }
}