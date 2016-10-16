using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;

namespace SinglePointTouch.Pages
{
  public partial class FingerPaintingPageMouseEvents : PhoneApplicationPage
  {
    private Rectangle _backgroundRectangle;
    private double _touchRadius = 20d;
    private bool ColorBackgroundMode = false;
    private int TouchPaintingSelectedColorIndex;

    public FingerPaintingPageMouseEvents()
    {
      InitializeComponent();

      _backgroundRectangle = BlankRectangle;
    }

    private void Rectangle_MouseMove(object sender, MouseEventArgs e)
    {
      foreach (StylusPoint p in e.StylusDevice.GetStylusPoints(DrawCanvas))
      {
        Ellipse ellipse = new Ellipse();
        ellipse.SetValue(Canvas.LeftProperty, p.X);
        ellipse.SetValue(Canvas.TopProperty, p.Y);
        ellipse.Opacity = p.PressureFactor;
        ellipse.Width = _touchRadius;
        ellipse.Height = _touchRadius;
        ellipse.IsHitTestVisible = false;
        ellipse.Stroke = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
        ellipse.Fill = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
        DrawCanvas.Children.Add(ellipse);
      }
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      ColorListBox.SelectedIndex = 0;

      //Setup memory tracking timer
      //DispatcherTimer TrackMemoryTimer = new DispatcherTimer();
      //TrackMemoryTimer.Interval = new TimeSpan(0, 0, 0, 0, 5000);
      //TrackMemoryTimer.Tick += DebugMemoryInfo_Tick;
      //TrackMemoryTimer.Start();
    }

    // Track memory info
    //void DebugMemoryInfo_Tick(object sender, EventArgs e)
    //{
    //  //GC.GetTotalMemory(true);
    //  long deviceTotalMemory = 
    //   (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
    //   "DeviceTotalMemory");
    //  long applicationCurrentMemoryUsage = 
    //   (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
    //   "ApplicationCurrentMemoryUsage");
    //  long applicationPeakMemoryUsage = 
    //   (long)Microsoft.Phone.Info.DeviceExtendedProperties.GetValue(
    //   "ApplicationPeakMemoryUsage");

    //  System.Diagnostics.Debug.WriteLine("--> " + 
    //    DateTime.Now.ToLongTimeString());
    //  System.Diagnostics.Debug.WriteLine("--> Device Total : " + 
    //    deviceTotalMemory.ToString());
    //  System.Diagnostics.Debug.WriteLine("--> App Current : " + 
    //    applicationCurrentMemoryUsage.ToString());
    //  System.Diagnostics.Debug.WriteLine("--> App Peak : " + 
    //    applicationPeakMemoryUsage.ToString());
    //}

    private void AppBarClearButton_Click(object sender, EventArgs e)
    {
      DrawCanvas.Children.Clear();
      DrawCanvas.Children.Add(BlankRectangle);
      BlankRectangle.Fill = new SolidColorBrush(Colors.White);
    }

    private void AppBarIncreaseButton_Click(object sender, EventArgs e)
    {
      if (_touchRadius <= 30d)
      {
        _touchRadius += 5;
      }
    }

    private void AppBarDecreaseButton_Click(object sender, EventArgs e)
    {
      if (_touchRadius > 20d)
      {
        _touchRadius -= 5;
      }
    }

    private void SetBackgroundColorMenuItem_Click(object sender, EventArgs e)
    {
      ColorListBox.Visibility = Visibility.Visible;
      ColorBackgroundMode = true;
      TouchPaintingSelectedColorIndex = ColorListBox.SelectedIndex;
    }

    private void ColorListBox_SelectionChanged(object sender, 
      SelectionChangedEventArgs e)
    {
      ColorListBox.Visibility = Visibility.Collapsed;
      if (ColorBackgroundMode == true)
      {
        _backgroundRectangle.Fill = 
          ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
        ColorBackgroundMode = false;
        ColorListBox.SelectedIndex = TouchPaintingSelectedColorIndex;
      }
    }

    private void AppBarChangeTouchColor_Click(object sender, EventArgs e)
    {
      ColorListBox.Visibility = Visibility.Visible;
    }
  }
}