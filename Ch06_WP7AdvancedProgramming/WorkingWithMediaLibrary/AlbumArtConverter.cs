using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace WorkingWithMediaLibrary.Converters
{
  public class AlbumArtConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, 
      System.Globalization.CultureInfo culture)
    {
      try
      {
        BitmapImage artImage = new BitmapImage();
        Album album = value as Album;
        if (album.HasArt)
        {
          artImage.SetSource(album.GetAlbumArt());
        }
        return artImage;
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
