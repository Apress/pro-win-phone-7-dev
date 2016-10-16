using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Controls;

namespace WebBrowserControl.pages
{
  public partial class WebBrowserControlScriptingPage : PhoneApplicationPage
  {
    public WebBrowserControlScriptingPage()
    {
      InitializeComponent();
      Loaded += new RoutedEventHandler(WebBrowserControlScriptingPage_Loaded);
    }

    void WebBrowserControlScriptingPage_Loaded(object sender, RoutedEventArgs e)
    {
      SetUpWebBrowserControlContent();
      webBrowserControl.Base = "home";
      webBrowserControl.Navigate(new Uri("content.html", UriKind.Relative));
    }

    private void SetUpWebBrowserControlContent()
    {
      //Copy content out of xap and into isolated storage
      using (IsolatedStorageFile isf =
              IsolatedStorageFile.GetUserStoreForApplication())
      {
        //if (!isf.DirectoryExists("home"))
        //{
        isf.CreateDirectory("home");
        //create base html file
        using (IsolatedStorageFileStream fs =
          isf.OpenFile("home/content.html", System.IO.FileMode.Create))
        {
          byte[] buffer = new byte[256];
          int count = 0;
          Stream resourceStream =
            Application.GetResourceStream(
                     new Uri("html/content.html", UriKind.Relative)).Stream;
          count = resourceStream.Read(buffer, 0, 256);
          while (count > 0)
          {
            fs.Write(buffer, 0, count);
            count = resourceStream.Read(buffer, 0, 256);
          }
        }
        //Create Image directory
        isf.CreateDirectory("home/images");
        //Create image file
        using (IsolatedStorageFileStream fs =
          isf.OpenFile("home/images/image.jpg", System.IO.FileMode.Create))
        {
          byte[] buffer = new byte[256];
          int count = 0;
          Stream resourceStream = Application.GetResourceStream(
            new Uri("images/image.jpg", UriKind.Relative)).Stream;
          count = resourceStream.Read(buffer, 0, 256);
          while (count > 0)
          {
            fs.Write(buffer, 0, count);
            count = resourceStream.Read(buffer, 0, 256);
          }
        }
      }
    }

    private void loadUrlAppBarButton_Click(object sender, EventArgs e)
    {
      //Invoke script
      webBrowserControl.InvokeScript(
            "PassData", "This is the data.  Hello from Silverlight.");
      webBrowserControl.InvokeScript(
            "SetImageSource", "images/image.jpg");
    }
  }
}