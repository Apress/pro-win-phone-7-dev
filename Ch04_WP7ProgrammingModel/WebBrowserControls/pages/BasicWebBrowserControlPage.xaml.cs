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

namespace WebBrowserControl.pages
{
  public partial class BasicWebBrowserControlPage : PhoneApplicationPage
  {
    public BasicWebBrowserControlPage()
    {
      InitializeComponent();
      webBrowserControl.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(webBrowserControl_LoadCompleted);
      webBrowserControl.Navigating += new EventHandler<NavigatingEventArgs>(webBrowserControl_Navigating);
    }

    void webBrowserControl_Navigating(object sender, NavigatingEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine(e.Uri);
    }

    private void loadUrlAppBarButton_Click(object sender, EventArgs e)
    {
      AnimateGreenRect.Begin();
      webBrowserControl.Navigate(new Uri(WebAddressTextBox.Text));
    }

    void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
      AnimateGreenRect.Stop();
    }
  }
}