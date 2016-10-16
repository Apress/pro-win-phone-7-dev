using GalaSoft.MvvmLight;
using VSMVideoPlayer.Model;
using System.Collections.Generic;
using System;

namespace VSMVideoPlayer.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    public const string VideosPropertyName = "Videos";
    private List<Video> _videos = null;
    public List<Video> Videos
    {
      get
      {
        return _videos;
      }

      set
      {
        if (_videos == value)
        {
          return;
        }
        var oldValue = _videos;
        _videos = value;
        RaisePropertyChanged(VideosPropertyName);
      }
    }

    public MainViewModel()
    {
      if (IsInDesignMode)
      {
        LoadVideos();
      }
      else
      {
        LoadVideos();
      }
    }

    private void LoadVideos()
    {
      _videos = new List<Video>()
      {
        new Video(){ Description="Tour De France parade video.", Name="TDF Video 1", Url=new Uri("/content/tdf1.wmv", UriKind.Relative)},
        new Video(){ Description="Tour De France peliton video.", Name="TDF Video 2", Url=new Uri("/content/tdf2.wmv", UriKind.Relative)},        
        new Video(){ Description="Paris skyline.", Name="Paris Video 1", Url=new Uri("/content/ParisSkyline.wmv", UriKind.Relative)}
      };
    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}