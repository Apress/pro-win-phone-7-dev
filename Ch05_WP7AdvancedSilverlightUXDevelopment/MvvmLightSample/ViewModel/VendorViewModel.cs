using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightSample.Models;

namespace MvvmLightSample.ViewModel
{
  public class VendorViewModel : ViewModelBase
  {
    public VendorViewModel()
    {
      if (IsInDesignMode)
      {
        LoadSampleData();
      }
      else
      {
        LoadData();
      }

      //Instantiate Commands
      AddAVendorCommand = new RelayCommand(
        () => AddVendor());

      RemoveAVendorCommand = new RelayCommand<Vendor>(
        param => RemoveVendor(param));

    }

    #region Design-time support
    private void LoadSampleData()
    {
      _vendors = new ObservableCollection<Vendor>()
      {
        new Vendor(){AccountNumber="111111", CreditRating=65,
          Name="DesignTime - Fabrikam Bikes" },
        new Vendor(){AccountNumber="222222", CreditRating=40,
          Name="Contoso Sports" },
        new Vendor(){AccountNumber="333333", CreditRating=30,
          Name="Duwamish Surfing Gear" },
        new Vendor(){AccountNumber="444444", CreditRating=65,
          Name="Contoso Bikes" },
        new Vendor(){AccountNumber="555555", CreditRating=40,
          Name="Fabrikam Sports" },
        new Vendor(){AccountNumber="666666", CreditRating=30,
          Name="Duwamish Golf" },
        new Vendor(){AccountNumber="777777", CreditRating=65,
          Name="Fabrikam Sun Sports" },
        new Vendor(){AccountNumber="888888", CreditRating=40,
          Name="Contoso Lacross" },
        new Vendor(){AccountNumber="999999", CreditRating=30,
          Name="Duwamish Team Sports" },
      };
    }
    #endregion

    #region Vendors Data Load
    HttpWebRequest httpWebRequest;
    private void LoadData()
    {
      httpWebRequest = HttpWebRequest.CreateHttp("http://localhost:9191/AdventureWorksRestJSON.svc/Vendors");
      httpWebRequest.BeginGetResponse(new AsyncCallback(GetVendors), null);
    }

    //add a reference to System.Servicemodel.web to get DataContractJsonSerializer
    void GetVendors(IAsyncResult result)
    {
      HttpWebResponse response = httpWebRequest.EndGetResponse(result) as HttpWebResponse;
      DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Vendor>));
      _vendors = ser.ReadObject(response.GetResponseStream()) as ObservableCollection<Vendor>;
      //Vendors is read-only so cannot set directly
      //Must call NotifyPropertyChanged notifications on UI thread
      //to update the UI and have data binding work properly
      Deployment.Current.Dispatcher.BeginInvoke(() =>
      {
        base.RaisePropertyChanged("Vendors");
      });
    }
    #endregion

    #region Vendors Business Logic
    private ObservableCollection<Vendor> _vendors;
    public ObservableCollection<Vendor> Vendors
    {
      get
      {
        return _vendors;
      }
      //set
      //{
      //  _vendors = value;
      //  NotifyPropertyChanged("Vendors");
      //}
    }

    public Vendor GetVendorByAccountNumber(string accountNumber)
    {
      var vendor = from v in _vendors
                   where v.AccountNumber == accountNumber
                   select v;

      return vendor.First<Vendor>();
    }

    public void AddVendor()
    {
      Vendors.Add(new Vendor()
      {
        AccountNumber = "111111",
        CreditRating = 65,
        Name = "Fabrikam Bikes - Added"
      });
    }

    public void RemoveVendor(object vendor)
    {
      if (null != vendor)
        Vendors.Remove((Vendor)vendor);
    }
    #endregion

    #region Commanding
    public RelayCommand AddAVendorCommand
    {
      get;
      private set;
    }

    public RelayCommand<Vendor> RemoveAVendorCommand
    {
      get;
      private set;
    }
    #endregion
  }
}