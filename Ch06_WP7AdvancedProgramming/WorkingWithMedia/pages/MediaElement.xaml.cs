using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Phone.Controls;

namespace WorkingWithMedia.pages
{
  public partial class MediaElement : PhoneApplicationPage
  {
    public MediaElement()
    {
      InitializeComponent();
      DispatcherTimer timer = new DispatcherTimer();
      timer.Tick += new EventHandler(timer_Tick);
      timer.Interval = new TimeSpan(0, 0, 1);
      timer.Start();
    }

    void timer_Tick(object sender, EventArgs e)
    {
      CanSeekTextBlock.Text = mediaPlayer.CanSeek.ToString();
      CanPauseTextBlock.Text = mediaPlayer.CanPause.ToString();
      DroppedFramesTextBlock.Text =
        mediaPlayer.DroppedFramesPerSecond.ToString();
    }

    private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
    {
      mediaPlayer.Play();
    }

    private void mediaPlayer_MediaFailed(object sender,
      ExceptionRoutedEventArgs e)
    {
      MessageBox.Show("Media Failed: " + e.ErrorException.Message);
    }

    private void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
    {

    }

    private void PlayAppBarBtn_Click(object sender, EventArgs e)
    {
      mediaPlayer.Source =
        new Uri("http://ecn.channel9.msdn.com/o9/ch9/8/9/6/6/3/5/WP7Xbox_ch9.wmv",
          UriKind.Absolute);
    }

    private void PauseAppBarBtn_Click(object sender, EventArgs e)
    {
      mediaPlayer.Pause();
    }

    private void mediaPlayer_CurrentStateChanged(object sender,
      RoutedEventArgs e)
    {
      CurrentStateTextBlock.Text = mediaPlayer.CurrentState.ToString();
    }

    private void mediaPlayer_BufferingProgressChanged(object sender,
      RoutedEventArgs e)
    {
      BufferingProgressTextBlock.Text = mediaPlayer.BufferingProgress.ToString();
    }

    private void mediaPlayer_DownloadProgressChanged(object sender,
      RoutedEventArgs e)
    {
      DownloadProgressChangedTextBlock.Text = mediaPlayer.DownloadProgress.ToString();
    }
  }
}