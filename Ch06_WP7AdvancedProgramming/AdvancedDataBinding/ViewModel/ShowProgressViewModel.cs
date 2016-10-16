using GalaSoft.MvvmLight;
using System.Windows;
using System.Net;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace AdvancedDataBinding.ViewModel
{
  public class ShowProgressViewModel : ViewModelBase
  {
    public ShowProgressViewModel()
    {
      if (IsInDesignMode)
      {
        CreateDesignTimeData();
      }

    }

    #region Design-timeData
    private void CreateDesignTimeData()
    {
      FeedItems = new List<FeedItem>(){
        new FeedItem(){Title="Feed Item 1", Description="Feed Item 1 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)},
        new FeedItem(){Title="Feed Item 2", Description="Feed Item 2 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)},
        new FeedItem(){Title="Feed Item 3", Description="Feed Item 3 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)},
        new FeedItem(){Title="Feed Item 4", Description="Feed Item 4 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)},
        new FeedItem(){Title="Feed Item 5", Description="Feed Item 5 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)},
        new FeedItem(){Title="Feed Item 6", Description="Feed Item 6 description goes here.  It is a long string of HTML.", Link=new Uri("http://create.msdn.com", UriKind.Absolute)}
      };
    }
    #endregion

    #region Run-time Data
    HttpWebRequest httpWebRequest;
    public void DownloadAppHubFeed()
    {
      Deployment.Current.Dispatcher.BeginInvoke(() =>
      {
        ShowProgressBar = true;
      });
      httpWebRequest = HttpWebRequest.CreateHttp("http://public.create.msdn.com/Feeds/CcoFeeds.svc/CmsFeed?group=Education Catalog List");
      httpWebRequest.BeginGetResponse(new AsyncCallback(ReceiveFeedData), null);
    }

    void ReceiveFeedData(IAsyncResult result)
    {
      HttpWebResponse response = httpWebRequest.EndGetResponse(result) as HttpWebResponse;
      using (StreamReader reader = new StreamReader(response.GetResponseStream()))
      {
        XDocument doc = XDocument.Parse(reader.ReadToEnd());
        var items = from results in doc.Descendants("item")
                    select new FeedItem
                    {
                      Title = results.Element("title").Value.ToString(),
                      Link = new Uri(results.Element("link").Value.ToString(), UriKind.Absolute),
                      Description = results.Element("description").Value.ToString()
                    };
        Deployment.Current.Dispatcher.BeginInvoke(() =>
        {
          FeedItems = items.ToList<FeedItem>();
          //Artificial delay to test progress bar
          System.Threading.Thread.Sleep(5000);
          ShowProgressBar = false;
        });
      }
    }
    #endregion

    public string ApplicationTitle
    {
      get { return "CHAPTER SIX - ADVANCED DATA BINDING"; }
    }

    public string PageName
    {
      get { return "show progress"; }
    }
    ////public override void Cleanup()
    ////{
    ////    // Clean own resources if needed

    ////    base.Cleanup();
    ////}

    public const string FeedItemsPropertyName = "FeedItems";
    private List<FeedItem> _feedItems = null;
    public List<FeedItem> FeedItems
    {
      get
      {
        return _feedItems;
      }

      set
      {
        if (_feedItems == value)
        {
          return;
        }
        var oldValue = _feedItems;
        _feedItems = value;
        RaisePropertyChanged(FeedItemsPropertyName);
      }
    }

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