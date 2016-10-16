using System;
using System.Windows.Data;

namespace AdvancedDataBinding.Converters
{
  public class HtmlToImageUriConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
      object parameter, System.Globalization.CultureInfo culture)
    {
      string html = (string)value;
      string imageUrl = "";
      if (null != html)
      {
        string[] strings = html.Split('"');
        if (strings.Length > 3)
          imageUrl = strings[3].Trim();
      }
      return imageUrl;
    }

    public object ConvertBack(object value, Type targetType,
      object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
