using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Controls;
using System.Windows;

namespace PhotosExtraApp.Converters
{
  public class VisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      try
      {
        Visibility visibility = Visibility.Visible;
        switch ((PageOrientation)value)
        {
          case PageOrientation.Landscape: visibility = Visibility.Collapsed;
            break;
          case PageOrientation.LandscapeLeft: visibility = Visibility.Collapsed;
            break;
          case PageOrientation.LandscapeRight: visibility = Visibility.Collapsed;
            break;
        }
        return visibility;
      }
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine(err.Message);
        return Visibility.Visible; ;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
