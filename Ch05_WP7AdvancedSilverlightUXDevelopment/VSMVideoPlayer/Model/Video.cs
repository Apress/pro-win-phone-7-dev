using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace VSMVideoPlayer.Model
{
  public class Video : INotifyPropertyChanged
  {
    private string _name;
    public string Name
    {
      get
      {return _name; }

      set
      {
        if (_name == value)
        {
          return;
        }
        var oldValue = _name;
        _name = value;
        RaisePropertyChanged("Name");
      }
    }

    private Uri _url;
    public Uri Url
    {
      get { return _url; }
      set 
      {
        if (_url == value)
        {
          return;
        }
        var oldValue = _url;
        _url = value;
        RaisePropertyChanged("Url");
      }
    }

    private string _description;
    public string Description
    {
      get { return _description; }
      set 
      {
        if (_description == value)
        {
          return;
        }
        var oldValue = _description;
        _description = value;
        RaisePropertyChanged("Description");
      }
    }
    

    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(String propertyName)
    {
      if (null != PropertyChanged)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
