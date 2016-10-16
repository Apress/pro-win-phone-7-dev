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
using Microsoft.Devices;
using System.IO;
using System.Windows.Resources;

namespace MusicPlusVideoHub
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      UpdateMusicPlusVideosHub_NewSection();
    }

    //This method fires when clicking "Back" from the VideoPlayerPage
    //as well as when application media is touched in the Music+Video Hub
    //such as in the 'New', 'History', or 'Now playing' section
    protected override void OnNavigatedTo(
      System.Windows.Navigation.NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

      if (NavigationContext.QueryString.Count > 0 &&
        NavigationContext.QueryString.ContainsKey("videoHub"))
      {
        NavigationService.Navigate(new Uri("/VideoPlayerPage.xaml?video=" +
          NavigationContext.QueryString["videoHub"], UriKind.Relative));
        //Remove this query string item so that when the user clicks 
        //"back" from the VideoPlayerPage the app doesn't loop back 
        //over to the VideoPlayerPage in an endless loop of navigation
        NavigationContext.QueryString.Remove("videoHub");
      }
    }

    private void PlayParisImage_MouseLeftButtonUp(object sender,
      MouseButtonEventArgs e)
    {
      NavigationService.Navigate(new Uri(
        "/VideoPlayerPage.xaml?video=Paris", UriKind.Relative));
    }

    private void PlayTDFImages_MouseLeftButtonUp(object sender,
      MouseButtonEventArgs e)
    {
      NavigationService.Navigate(new Uri("/VideoPlayerPage.xaml?video=TDF",
        UriKind.Relative));
    }

    #region Music+Video Hub Integration
    private void UpdateMusicPlusVideosHub_NewSection()
    {
      //Update "New" section of the Music+Videos Hub for the two videos
      //Hard coded in the app for the sample to demonstrate feature
      //Would dynamically obtain this via remote service calls otherwise

      //NEW Paris Video
      //Create ImageStream for Paris video from the embedded content
      //Set Build Action to "Content" not "Embedded Resource"
      StreamResourceInfo ParisTileStreamResource =
         Application.GetResourceStream(new Uri("images/hubTiles/ParisTile.jpg",
           UriKind.Relative));
      //Create the MediaHistoryItem that has been newly aquired
      MediaHistoryItem ParisVideoMediaHistoryItem = new MediaHistoryItem();
      ParisVideoMediaHistoryItem.ImageStream = ParisTileStreamResource.Stream;
      ParisVideoMediaHistoryItem.Source = "xap";
      ParisVideoMediaHistoryItem.Title = "Paris Skyline Video";
      //Set State for situation when navigating via click in Media+Videos Hub
      ParisVideoMediaHistoryItem.PlayerContext.Add("videoHub", "Paris");
      //This method call writes the history item to the 'New' section
      MediaHistory.Instance.WriteAcquiredItem(ParisVideoMediaHistoryItem);

      //NEW Tour De France Video
      StreamResourceInfo tdfTileStreamResource =
      Application.GetResourceStream(new Uri("images/hubTiles/TDFTile.jpg",
        UriKind.Relative));
      //Create the MediaHistoryItem that has been newly aquired
      MediaHistoryItem tdfVideoMediaHistoryItem = new MediaHistoryItem();
      tdfVideoMediaHistoryItem.ImageStream = tdfTileStreamResource.Stream;
      tdfVideoMediaHistoryItem.Source = "xap";
      tdfVideoMediaHistoryItem.Title = "Tour De France Video";
      //Set State for situation when navigating via click in Media+Videos Hub
      tdfVideoMediaHistoryItem.PlayerContext.Add("videoHub", "TDF");
      //This method call writes the history item to the 'New' section
      MediaHistory.Instance.WriteAcquiredItem(tdfVideoMediaHistoryItem);

    }
    #endregion
  }
}