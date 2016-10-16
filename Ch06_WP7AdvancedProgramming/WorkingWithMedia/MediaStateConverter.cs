using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WorkingWithMedia.Converters
{
  public class MediaStateConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      try
      {
        MediaElementState state = (MediaElementState)value;
        bool showProgress = false;
        switch (state)
        {
          case MediaElementState.AcquiringLicense: showProgress = true;
            break;
          case MediaElementState.Buffering:showProgress = true;
            break;
          case MediaElementState.Closed:showProgress = false;
            break;
          case MediaElementState.Individualizing:showProgress = true;
            break;
          case MediaElementState.Opening:showProgress = true;
            break;
        }
        return showProgress;
      }
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine(err.Message);
        return false;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter,
      System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
