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

namespace LocationSensorSilverlight
{
  public partial class MainPage : PhoneApplicationPage
  {
    GeoCoordinateWatcher LocationService;
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      LocationService = new GeoCoordinateWatcher();
      LocationService.PositionChanged += 
        new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>
          (LocationSensor_PositionChanged);

      LocationService.StatusChanged += 
        new EventHandler<GeoPositionStatusChangedEventArgs>
          (LocationSensor_StatusChanged);
   }

    void LocationSensor_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
    {
      LocationSensorStatusText.Text = e.Status.ToString();
    }

    void LocationSensor_PositionChanged
      (object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
    {
      LatitudeText.Text = String.Format("{0:0.00}", e.Position.Location.Latitude);
      LongitudeText.Text = String.Format("{0:0.00}", e.Position.Location.Longitude);
      AltitudeText.Text = e.Position.Location.Altitude.ToString();
      TimestampText.Text = e.Position.Timestamp.ToLocalTime().ToString();
    }

    private void SetAccuracy_Click(object sender, EventArgs e)
    {
      if (LocationService.DesiredAccuracy == GeoPositionAccuracy.Default)
        if (MessageBox.Show(
          "Current Accuracy is Default.  Change accuracy to High?"
          +"This may take some time and will consume additional battery power.",
          "Change Location Accuracy", MessageBoxButton.OKCancel)
          == MessageBoxResult.OK)
        {
          LocationService.Dispose();
          LocationService = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
          LocationService.PositionChanged += 
        new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>
          (LocationSensor_PositionChanged);

      LocationService.StatusChanged += 
        new EventHandler<GeoPositionStatusChangedEventArgs>
          (LocationSensor_StatusChanged);
        }
      else
        if (MessageBox.Show(
          "Current Accuracy is High.  Change accuracy to Default?"+
          "This wll be faster but will reduce accuracy.",
          "Change Location Accuracy", MessageBoxButton.OKCancel) 
          == MessageBoxResult.OK)
        {
          LocationService.Dispose();
          LocationService = 
            new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
          LocationService.PositionChanged += 
        new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>
          (LocationSensor_PositionChanged);

        LocationService.StatusChanged += 
          new EventHandler<GeoPositionStatusChangedEventArgs>
            (LocationSensor_StatusChanged);
          }
    }

    private void StopLocation_Click(object sender, EventArgs e)
    {
      LocationService.Stop();
    }

    private void StartLocation_Click(object sender, EventArgs e)
    {
      LocationService.Start();
    }

    private void PlotLocation_Click(object sender, EventArgs e)
    {
      BingMap.Center = LocationService.Position.Location;
      BingMap.ZoomLevel = 15;
    }

    private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
    {
      Border border = sender as Border;
      CompositeTransform compositeTransform = border.RenderTransform as CompositeTransform;
      compositeTransform.TranslateX += e.HorizontalChange;
      compositeTransform.TranslateY += e.VerticalChange;
      e.Handled = true;
    }
  }
}