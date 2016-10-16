using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;

namespace AdvancedDataBinding.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    public string ApplicationTitle
    {
      get { return "CHAPTER SIX";}
    }

    public string PageName
    {
      get {return "advanced data binding";}
    }

    public const string PagesPropertyName = "Pages";
    private List<PageItemViewModel> _pages = null;
    public List<PageItemViewModel> Pages
    {
      get
      {
        return _pages;
      }

      protected set
      {
        if (_pages == value)
        {
          return;
        }

        var oldValue = _pages;
        _pages = value;

        // Update bindings, no broadcast
        RaisePropertyChanged(PagesPropertyName);
      }
    }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
      if (IsInDesignMode)
      {
        LoadPagesInfo();
      }
      else
      {
        LoadPagesInfo();
      }
    }

    private void LoadPagesInfo()
    {
      Pages = new List<PageItemViewModel>()
      {
        new PageItemViewModel(){PageTitle="syndicated services",
          PageUri=new Uri("/View/SyndicatedServices.xaml",UriKind.Relative)},
        new PageItemViewModel(){PageTitle="showing progress",
          PageUri=new Uri("/View/ShowingProgress.xaml",UriKind.Relative)},
        new PageItemViewModel(){PageTitle="lazy load images",
          PageUri=new Uri("/View/LazyLoadImages.xaml",UriKind.Relative)},
        new PageItemViewModel(){PageTitle="data binding to anything",
          PageUri=new Uri("/View/DatabindToAnything.xaml",UriKind.Relative)}
      };
    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}