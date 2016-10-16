using GalaSoft.MvvmLight;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace PhotosExtraApp.ViewModel
{
  /// <summary>
  /// This class contains properties that the main View can data bind to.
  /// <para>
  /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
  /// </para>
  /// <para>
  /// You can also use Blend to data bind with the tool's support.
  /// </para>
  /// <para>
  /// See http://www.galasoft.ch/mvvm/getstarted
  /// </para>
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    MediaLibrary _mediaLibrary;
    public string ApplicationTitle
    {
      get
      {
        return "CHAPTER SIX";
      }
    }

    public string PageName
    {
      get
      {
        return "photos extra";
      }
    }

    #region Pictures Property
    public const string PicturesPropertyName = "Pictures";
    private List<Picture> _pictures = null;
    public List<Picture> Pictures
    {
      get
      {
        return _pictures;
      }

      set
      {
        if (_pictures == value)
        {
          return;
        }

        var oldValue = _pictures;
        _pictures = value;
        RaisePropertyChanged(PicturesPropertyName);
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
      _mediaLibrary = new MediaLibrary();
      if (IsInDesignMode)
      {
        //CreateDataCollections();
      }
      else
      {
        CreateDataCollections();
      }
    }

    #region run-time data
    private void CreateDataCollections()
    {
      Pictures = new List<Picture>();
      Picture picture = null;
      for (int i = 0; i < _mediaLibrary.Pictures.Count; i++)
      {
        picture = _mediaLibrary.Pictures[i];
        Pictures.Add(picture);
        if (i > 30)
          break;
      }
    }
    #endregion

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}