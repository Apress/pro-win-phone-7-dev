using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
namespace AdvancedDataBinding.ViewModel
{
  public class PageItemViewModel : ViewModelBase
  {
    public PageItemViewModel()
    {
      //Wire up the NavigateToPageCommand RelayCommand to send the message
      //for the Uri of the page to navigate to.
      NavigateToPageCommand = new RelayCommand<Uri>(
       param => SendNavigationRequestMessage(param));
    }

    #region PageItem properties
    public const string PageTitlePropertyName = "PageTitle";
    private string _pageTitle = null;
    public string PageTitle
    {
      get
      {
        return _pageTitle;
      }
      set
      {
        if (_pageTitle == value)
        {
          return;
        }

        var oldValue = _pageTitle;
        _pageTitle = value;
        RaisePropertyChanged(PageTitlePropertyName);
      }
    }


    public const string PageUriPropertyName = "PageUri";
    private Uri _pageUri = null;
    public Uri PageUri
    {
      get
      {
        return _pageUri;
      }
      set
      {
        if (_pageUri == value)
        {
          return;
        }

        var oldValue = _pageUri;
        _pageUri = value;

        // Update bindings, no broadcast
        RaisePropertyChanged(PageUriPropertyName);
      }
    }
    #endregion

    #region Commanding and Messaging
    public RelayCommand<Uri> NavigateToPageCommand
    {
      get;
      private set;
    }

    protected void SendNavigationRequestMessage(Uri uri)
    {
      Messenger.Default.Send<Uri>(uri, "PageNavRequest");
    }
    #endregion
  }
}
