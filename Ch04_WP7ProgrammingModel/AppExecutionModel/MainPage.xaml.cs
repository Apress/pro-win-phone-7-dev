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
using RestJSON.DataModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Diagnostics;
using Microsoft.Phone.Shell;

namespace AppExecutionModel
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      Debug.WriteLine("In MainPage Constructor");
    }

    #region Get Raw JSON data
    HttpWebRequest request;
    private void GetRawJSONDataAppBarBtn_Click(object sender, EventArgs e)
    {
      request = WebRequest.CreateHttp("http://localhost:9191/AdventureWorksRestJSON.svc/Vendors");
      request.BeginGetResponse(new AsyncCallback(ReceiveRawJSONData), null);
    }

    string returnedResult;
    void ReceiveRawJSONData(IAsyncResult result)
    {
      WebResponse response = request.EndGetResponse(result);
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
      request = WebRequest.CreateHttp("http://localhost:9191/AdventureWorksRestJSON.svc/Vendors");
      request.BeginGetResponse(new AsyncCallback(GetVendors), null);
    }

    //add a reference to System.Servicemodel.web to get DataContractJsonSerializer
    void GetVendors(IAsyncResult result)
    {
      DataContractJsonSerializer ser = null;
      WebResponse response = request.EndGetResponse(result);
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

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      Debug.WriteLine("In PhoneApplicationPage_Loaded");
    }

    private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
    {
      Debug.WriteLine("In PhoneApplicationPage_Unloaded");
    }

    private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
    {
      Debug.WriteLine("In PhoneApplicationPage_BackKeyPress");
    }

    protected override void OnNavigatedFrom(
      System.Windows.Navigation.NavigationEventArgs e)
    {
      base.OnNavigatedFrom(e);
      IDictionary<string, object> state = PhoneApplicationService.Current.State;
      state["Selected Item"] = VendorsListBox.SelectedIndex;
      DataStore.Instance.SaveCollection<ObservableCollection<Vendor>>(
        DataStore.Instance.Vendors, "Vendors");
    }

    protected override void OnNavigatedTo(
      System.Windows.Navigation.NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      if (PhoneApplicationService.Current.StartupMode ==
        StartupMode.Activate)
      {
        //Load data from isolated storage
        DataStore.Instance.Vendors =
        DataStore.Instance.LoadCollection<ObservableCollection<Vendor>>(
        DataStore.Instance.Vendors, "Vendors");
        VendorsListBox.ItemsSource = DataStore.Instance.Vendors;
        // The state bag for  temporary state
        IDictionary<string, object> state =
          PhoneApplicationService.Current.State;
        // See if the bag contains the selected item
        if (state.ContainsKey("Selected Item"))
        {
          //Set selected item on page
          VendorsListBox.SelectedIndex = (int)state["Selected Item"];
          if (VendorsListBox.SelectedIndex != -1)
          //Scroll to selected item
          VendorsListBox.ScrollIntoView(
            VendorsListBox.Items[VendorsListBox.SelectedIndex]);
        }
      }
    }
  }
}