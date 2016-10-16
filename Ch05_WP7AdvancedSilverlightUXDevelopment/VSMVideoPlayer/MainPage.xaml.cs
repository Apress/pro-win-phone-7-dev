using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace VSMVideoPlayer
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void rewindAppBarBtn_Click(object sender, System.EventArgs e)
    {
      if (mediaPlayer.CanSeek)
      {
        mediaPlayer.Position = mediaPlayer.Position - new TimeSpan(0, 0, 5);
        mediaPlayer.Play();
      }
    }

    private void stopAppBarBtn_Click(object sender, System.EventArgs e)
    {
      mediaPlayer.Pause();
    }

    private void playAppBarBtn_Click(object sender, System.EventArgs e)
    {

      mediaPlayer.Play();
    }

    private void ffAppBarBtn_Click(object sender, System.EventArgs e)
    {

      if (mediaPlayer.CanSeek)
      {
        mediaPlayer.Position = mediaPlayer.Position + new TimeSpan(0, 0, 5);
        mediaPlayer.Play();
      }
    }

    private void mediaPlayer_MediaFailed(object sender, 
      System.Windows.ExceptionRoutedEventArgs e)
    {
      MessageBox.Show("Media Failed: " + e.ErrorException.Message);
    }

    private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
    {
      VisualStateManager.GoToState(this, e.Orientation.ToString(), true); 

    }
  }
}
