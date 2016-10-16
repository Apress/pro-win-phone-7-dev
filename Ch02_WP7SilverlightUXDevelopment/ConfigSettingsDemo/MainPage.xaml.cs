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

namespace ConfigSettingsDemo
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void divideByZeroBtn_Click(object sender, RoutedEventArgs e)
    {
      int numerator = 1;
      int denominator = 0;
      Double value = numerator / denominator;
    }

    private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
    {
      switch (e.Orientation)
      {
        case PageOrientation.Landscape: 
          divideByZeroBtn.Margin = new Thickness(12, 200, 235, 0);
          break;
        case PageOrientation.LandscapeLeft:
          divideByZeroBtn.Margin = new Thickness(12, 200, 235, 0);
          break;
        case PageOrientation.LandscapeRight:
          divideByZeroBtn.Margin = new Thickness(12, 200, 235, 0);
          break;
        //default is Portrait
        default: 
          divideByZeroBtn.Margin = new Thickness(12, 400, 235, 0);
          break;
      }
    }
  }
}