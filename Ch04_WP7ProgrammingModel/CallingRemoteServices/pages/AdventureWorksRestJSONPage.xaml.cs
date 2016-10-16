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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using RestJSON.DataModel;

namespace CallingRemoteServices.pages
{
  public partial class dventureWorksRestJSONPage : PhoneApplicationPage
  {
    public dventureWorksRestJSONPage()
    {
      InitializeComponent();
    }

    #region Get Raw JSON data
    HttpWebRequest httpWebRequest;
    private void GetRawJSONDataAppBarBtn_Click(object sender, EventArgs e)
    {
      httpWebRequest = HttpWebRequest.CreateHttp("http://localhost:9191/AdventureWorksRestJSON.svc/Vendors");
      httpWebRequest.BeginGetResponse(new AsyncCallback(ReceiveRawJSONData), null);
    }

    string returnedResult;
    void ReceiveRawJSONData(IAsyncResult result)
    {
      HttpWebResponse response = httpWebRequest.EndGetResponse(result) as HttpWebResponse;
      using (StreamReader reader = new StreamReader(response.GetResponseStream()))
      {
        returnedResult = reader.ReadToEnd();
      }
      LayoutRoot.Dispatcher.BeginInvoke(() =>
      {
        MessageBox.Show(returnedResult);
      });
    }
    #endregion

    #region Retrieve Vendors Data
    private void GetVendorsAppbarBtn_Click(object sender, EventArgs e)
    {
      httpWebRequest = HttpWebRequest.CreateHttp("http://localhost:9191/AdventureWorksRestJSON.svc/Vendors");
      httpWebRequest.BeginGetResponse(new AsyncCallback(GetVendors), null);
    }

    //add a reference to System.Servicemodel.web to get DataContractJsonSerializer
    void GetVendors(IAsyncResult result)
    {
      DataContractJsonSerializer ser = null;
      HttpWebResponse response = httpWebRequest.EndGetResponse(result) as HttpWebResponse;
      ser = new DataContractJsonSerializer(typeof(ObservableCollection<Vendor>));
      DataStore.Instance.Vendors = ser.ReadObject(response.GetResponseStream()) as ObservableCollection<Vendor>;
      VendorsListBox.Dispatcher.BeginInvoke(() =>
      {
        VendorsListBox.ItemsSource = DataStore.Instance.Vendors;
      });
    }
    #endregion

    private void SaveVendorsAppbarBtn_Click(object sender, EventArgs e)
    {
      DataStore.Instance.SaveCollection<ObservableCollection<Vendor>>(
        DataStore.Instance.Vendors, "Vendors");
    }

    private void LoadVendorsAppbarBtn_Click(object sender, EventArgs e)
    {
      DataStore.Instance.Vendors = 
        DataStore.Instance.LoadCollection<ObservableCollection<Vendor>>(
        DataStore.Instance.Vendors, "Vendors");
      VendorsListBox.ItemsSource = DataStore.Instance.Vendors;
    }

    private void addVendorButton_Click(object sender, RoutedEventArgs e)
    {
      if (DataStore.Instance.Vendors != null)
      {
        DataStore.Instance.Vendors.Add(
        new Vendor()
        {
          AccountNumber = "555555",
          CreditRating = 45,
          Name = "Frabrikam Sports"
        });
      }
    }

    private void deleteButton_Click(object sender, RoutedEventArgs e)
    {
      if (DataStore.Instance.Vendors != null)
      {
        DataStore.Instance.Vendors.Remove((Vendor)VendorsListBox.SelectedItem);
      }
    }
  }
}