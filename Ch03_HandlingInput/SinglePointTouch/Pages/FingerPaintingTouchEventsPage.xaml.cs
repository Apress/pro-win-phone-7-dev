using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SinglePointTouch.Pages
{
  public partial class FingerPaintingPageTouchEvents : PhoneApplicationPage
  {
    private Rectangle _backgroundRectangle;
    private double _touchRadius = 20d;
    private bool ColorBackgroundMode = false;
    private int TouchPaintingSelectedColorIndex;
    private bool InDrawingMode = false ;

    public FingerPaintingPageTouchEvents()
    {
      InitializeComponent();
      _backgroundRectangle = BlankRectangle;
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      ColorListBox.SelectedIndex = 0;

      System.Windows.Input.Touch.FrameReported +=
        new TouchFrameEventHandler(Touch_FrameReported);
    }

    private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
    {
      System.Windows.Input.Touch.FrameReported -= Touch_FrameReported;
    }

    void Touch_FrameReported(object sender, TouchFrameEventArgs e)
    {
      foreach (TouchPoint p in e.GetTouchPoints(DrawCanvas))
      {
        if ((InDrawingMode) && (p.Action == TouchAction.Move))
        {
          Ellipse ellipse = new Ellipse();
          ellipse.SetValue(Canvas.LeftProperty, p.Position.X);
          ellipse.SetValue(Canvas.TopProperty, p.Position.Y);
          ellipse.Width = _touchRadius;
          ellipse.Height = _touchRadius;
          ellipse.IsHitTestVisible = false;
          ellipse.Stroke = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
          ellipse.Fill = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
          DrawCanvas.Children.Add(ellipse);
        }
      }
    }

    //private void Rectangle_MouseMove(object sender, MouseEventArgs e)
    //{
    //  foreach (StylusPoint p in e.StylusDevice.GetStylusPoints(DrawCanvas))
    //  {
    //    Ellipse ellipse = new Ellipse();
    //    ellipse.SetValue(Canvas.LeftProperty, p.X);
    //    ellipse.SetValue(Canvas.TopProperty, p.Y);
    //    ellipse.Opacity = p.PressureFactor;
    //    ellipse.Width = _touchRadius;
    //    ellipse.Height = _touchRadius;
    //    ellipse.IsHitTestVisible = false;
    //    ellipse.Stroke = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
    //    ellipse.Fill = ((ColorClass)ColorListBox.SelectedItem).ColorBrush;
    //    DrawCanvas.Children.Add(ellipse);
    //  }
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
      InDrawingMode = false ;
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
      InDrawingMode = true;
    }

    private void AppBarChangeTouchColor_Click(object sender, EventArgs e)
    {
      InDrawingMode = false;
      ColorListBox.Visibility = Visibility.Visible;
    }
  }
}