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
using Microsoft.Xna.Framework.Media;

namespace WorkingWithMediaLibrary
{
  public partial class MainPanoramaPage : PhoneApplicationPage
  {
    public MainPanoramaPage()
    {
      InitializeComponent();
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      object obj = (sender as ListBox).SelectedItem;
      MediaLibrary library = new MediaLibrary();
      try
      {
        switch (obj.GetType().ToString())
        {
          case "Microsoft.Xna.Framework.Media.Album": MediaPlayer.Play(((Album)(obj)).Songs); break;
          case "Microsoft.Xna.Framework.Media.Song": MediaPlayer.Play((Song)(obj)); break;
          case "Microsoft.Xna.Framework.Media.Playlist": MediaPlayer.Play(((Playlist)(obj)).Songs); break;
          case "Microsoft.Xna.Framework.Media.Artist": MediaPlayer.Play(((Artist)(obj)).Songs); break;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message); 
      };
    }
  }
}