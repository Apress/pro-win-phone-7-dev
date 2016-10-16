using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Resources;
using Microsoft.Devices;

namespace MusicPlusVideoHub
{
  public partial class VideoPlayerPage : PhoneApplicationPage
  {
    public VideoPlayerPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      VideoPlayer.Source =
        new Uri(@"/videos/" + NavigationContext.QueryString["video"] + ".wmv", UriKind.Relative);

      base.OnNavigatedTo(e);
    }
    #region MediaElement Events
    private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
    {
      //Update Now playing
      UpdateMusicPlusVideoAppNowPlaying(NavigationContext.QueryString["video"]);

      //Update History
      UpdateMusicPlusVideoAppHistory(NavigationContext.QueryString["video"]);

      VideoPlayer.Play();
    }

    private void VideoPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
    {
      MessageBox.Show(e.ErrorException.Message);
    }
    #endregion

    #region Music+Video Hub Integration
    private void UpdateMusicPlusVideoAppNowPlaying(string video)
    {
      StreamResourceInfo TileStreamResource =
        Application.GetResourceStream(new Uri("images/hubTiles/" + video + "NowPlayingTile.jpg", UriKind.Relative));
      //Create the MediaHistoryItem that is playing
      MediaHistoryItem VideoMediaHistoryItem = new MediaHistoryItem();
      VideoMediaHistoryItem.ImageStream = TileStreamResource.Stream;
      VideoMediaHistoryItem.Source = "xap";
      VideoMediaHistoryItem.Title = "video " + video;
      //Set State for situation when navigating via click in Media+Videos Hub
      VideoMediaHistoryItem.PlayerContext.Add("videoHub", video);
      //This method call writes the history item to the 'Now playing' section
      MediaHistory.Instance.NowPlaying = VideoMediaHistoryItem;
    }

    private void UpdateMusicPlusVideoAppHistory(string video)
    {
      StreamResourceInfo TileStreamResource =
      Application.GetResourceStream(new Uri("images/hubTiles/" + video + "Tile.jpg", UriKind.Relative));
      //Create the MediaHistoryItem that was recently played
      MediaHistoryItem VideoMediaHistoryItem = new MediaHistoryItem();
      VideoMediaHistoryItem.ImageStream = TileStreamResource.Stream;
      VideoMediaHistoryItem.Source = "xap";
      VideoMediaHistoryItem.Title = "video " + video;
      //Set State for situation when navigating via click in Media+Videos Hub
      VideoMediaHistoryItem.PlayerContext.Add("videoHub", video);
      //This method call writes the history item to the 'History' section
      MediaHistory.Instance.WriteRecentPlay(VideoMediaHistoryItem);
    }
    #endregion

  }
}