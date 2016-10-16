using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Navigation;

namespace AdvancedDataBinding
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      Messenger.Default.Register<Uri>(
        this, "PageNavRequest", 
        (uri) => NavigationService.Navigate(uri));
    }

  }
}
