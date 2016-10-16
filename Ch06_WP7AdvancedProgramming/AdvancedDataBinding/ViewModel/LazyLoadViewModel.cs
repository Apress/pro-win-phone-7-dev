using GalaSoft.MvvmLight;
using System.Windows;
using System.Net;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using NetflixCatalog.Model;
using System.Data.Services.Client;

namespace AdvancedDataBinding.ViewModel
{
  public class LazyLoadViewModel : ViewModelBase
  {
    private NetflixCatalog.Model.NetflixCatalog ODataContext;
    public LazyLoadViewModel()
    {
      if (IsInDesignMode)
      {
        CreateDesignTimeData();
      }

      ODataContext = new NetflixCatalog.Model.NetflixCatalog(
        new Uri("http://odata.netflix.com/Catalog/",
             UriKind.Absolute));
    }

    #region Design-timeData
    private void CreateDesignTimeData()
    {
    }
    #endregion

    #region Run-time Data
    private string TopMovieTitlesPropertyName = "TopMovieTitles";
    private DataServiceCollection<Title> topMovieTitles;
    public DataServiceCollection<Title> TopMovieTitles
    {
      get { return topMovieTitles; }
      private set
      {
        if (null == value)
          return;
        topMovieTitles = value;
        RaisePropertyChanged(TopMovieTitlesPropertyName);
      }
    }

    public void DownloadNetflixTopTitles()
    {
      Deployment.Current.Dispatcher.BeginInvoke(() =>
      {
        ShowProgressBar = true;
      });
      topMovieTitles = new DataServiceCollection<Title>(ODataContext);
      topMovieTitles.LoadCompleted += 
        new EventHandler<LoadCompletedEventArgs>(topMovieTitles_LoadCompleted);
      topMovieTitles.LoadAsync(new Uri("/Titles()",UriKind.Relative));

     }

    void topMovieTitles_LoadCompleted(object sender, LoadCompletedEventArgs e)
    {
      Deployment.Current.Dispatcher.BeginInvoke(() =>
      {
        ShowProgressBar = false;
        TopMovieTitles = topMovieTitles;
      });
    }
    
    #endregion

    public string ApplicationTitle
    {
      get { return "CHAPTER SIX - ADVANCED DATA BINDING"; }
    }

    public string PageName
    {
      get { return "lazy load images"; }
    }
    ////public override void Cleanup()
    ////{
    ////    // Clean own resources if needed

    ////    base.Cleanup();
    ////}

    
    public const string ShowProgressBarPropertyName = "ShowProgressBar";
    private bool _showProgressBar = false;
    public bool ShowProgressBar
    {
      get
      {
        return _showProgressBar;
      }

      set
      {
        if (_showProgressBar == value)
        {
          return;
        }

        var oldValue = _showProgressBar;
        _showProgressBar = value;
        RaisePropertyChanged(ShowProgressBarPropertyName);
      }
    }
  }
}