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
using Microsoft.Phone.Tasks;

namespace WorkingWithMedia.pages
{
  public partial class MediaPlayeTask : PhoneApplicationPage
  {
    public MediaPlayeTask()
    {
      InitializeComponent();
    }

    private void textBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
      mediaPlayerLauncher.Controls = MediaPlaybackControls.FastForward |
        MediaPlaybackControls.Pause | MediaPlaybackControls.Rewind |
        MediaPlaybackControls.Skip | MediaPlaybackControls.Stop;
      mediaPlayerLauncher.Location = MediaLocationType.Data;
      mediaPlayerLauncher.Media = new Uri("http://ecn.channel9.msdn.com/o9/ch9/8/9/6/6/3/5/WP7Xbox_ch9.mp4");
      mediaPlayerLauncher.Show();

    }
  }
}