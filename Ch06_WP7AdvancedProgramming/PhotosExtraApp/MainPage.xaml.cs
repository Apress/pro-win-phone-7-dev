using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Media;

namespace PhotosExtraApp
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      //Process query string
      IDictionary<string, string> queryStrings = this.NavigationContext.QueryString;
      if (NavigationContext.QueryString.Count > 0 &&
        NavigationContext.QueryString.ContainsKey("token"))
      {
        MediaLibrary library = new MediaLibrary();
        Picture picture = library.GetPictureFromToken(queryStrings["token"]);
        //Remove this query string item so that when the user clicks 
        //"back" from the ImageEditorView page the app doesn't loop back 
        //over to the ImageEditorView in an endless loop of navigation because
        //the query string value is still present and picked up by
        //MainPage.OnNavigateTo each time...
        NavigationContext.QueryString.Remove("token");

        //Send Message with Picture object
        SetPictureAndNavigate(picture);
      }
    }

    private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
      ListBox lb = sender as ListBox;
      SetPictureAndNavigate(lb.SelectedItem as Picture);
    }

    void SetPictureAndNavigate(Picture picture)
    {
      Messenger.Default.Send<Picture>(picture, "PictureToEdit");
      NavigationService.Navigate(new Uri("/PictureEditorView.xaml", UriKind.Relative));
    }
  }
}
