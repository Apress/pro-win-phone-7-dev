using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace PhotosExtraApp.Converters
{
  public class ImageNameConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      try
      {
        //return the first part of the file name
        //break at the period
        return ((string)value).Split('.')[0];
      }
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine(err.Message);
        return "";
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
