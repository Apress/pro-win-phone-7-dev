using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Windows;
using Microsoft.Phone.Controls;
using PhoneNetworkApi = Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Marketplace;
namespace DeviceInfo
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      RetrieveIdentityValues();
      RetrieveDeviceInfo();
      RetrieveSystemEnvironmentInfo();
      SetupNetworkStatusCheck();
      ApplicationLicenseAndTrialInfo();
    }

    #region Retrieve Identity
    private void RetrieveIdentityValues()
    {
      object deviceID;
      object userID;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceUniqueID", out deviceID))
        MessageBox.Show("Error retrieving Device iD");
      else
        DeviceIDTextBlock.Text = (string)deviceID;

      if (!Microsoft.Phone.Info.UserExtendedProperties.TryGetValue("ANID", out userID))
        MessageBox.Show("Error retrieving User iD");
      else
        UserIDTextBlock.Text = (string)userID;
    }
    #endregion

    #region DeviceInfo
    private void RetrieveDeviceInfo()
    {
      object deviceManufacturer;
      object deviceName;
      object deviceFirmwareVersion;
      object deviceHardwareVersion;
      object deviceTotalMemory;
      object applicationCurrentMemoryUsage;
      object applicationPeakMemoryUsage;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceManufacturer", out deviceManufacturer))
        MessageBox.Show("Error retrieving Device Manufacturer");
      else
        DeviceManufacturerTextBlock.Text = (string)deviceManufacturer;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceName", out deviceName))
        MessageBox.Show("Error retrieving Device Name");
      else
        DeviceNameTextBlock.Text = (string)deviceName;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceFirmwareVersion", out deviceFirmwareVersion))
        MessageBox.Show("Error retrieving Device Firmware Version");
      else
        DeviceFirmwareVersionTextBlock.Text = (string)deviceFirmwareVersion;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceHardwareVersion", out deviceHardwareVersion))
        MessageBox.Show("Error retrieving Device Hardware Version");
      else
        DeviceHardwareVersionTextBlock.Text = (string)deviceHardwareVersion;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceTotalMemory", out deviceTotalMemory))
        MessageBox.Show("Error retrieving Device Total Memory");
      else
        DeviceTotalMemoryTextBlock.Text = ((long)deviceTotalMemory).ToString("#,#", CultureInfo.InvariantCulture) ;

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("ApplicationCurrentMemoryUsage", out applicationCurrentMemoryUsage))
        MessageBox.Show("Error retrieving Application CurrentMemory Usage");
      else
        ApplicationCurrentMemoryUsageTextBlock.Text = ((long)applicationCurrentMemoryUsage).ToString("#,#", CultureInfo.InvariantCulture);

      if (!Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("ApplicationPeakMemoryUsage", out applicationPeakMemoryUsage))
        MessageBox.Show("Error retrieving Application Peak Memory Usage");
      else
        ApplicationPeakMemoryUsageTextBlock.Text = ((long)applicationPeakMemoryUsage).ToString("#,#", CultureInfo.InvariantCulture);
    }
    #endregion

    #region Environment Info
    private void RetrieveSystemEnvironmentInfo()
    {
      try
      {
        CurrentDirectoryTextBlock.Text = Environment.CurrentDirectory;
      }
      catch
      {
        CurrentDirectoryTextBlock.Text = "Cannot access current directory";
      }
      HasShutdownStartedTextBlock.Text = Environment.HasShutdownStarted.ToString();
      OSVersionPlatformTextBlock.Text = Environment.OSVersion.Platform.ToString();
      OSVersionVersionTextBlock.Text = Environment.OSVersion.Version.ToString();
      TickCountTextBlock.Text = Environment.TickCount.ToString();
      CLRVersionTextBlock.Text = Environment.Version.ToString();
    }
    #endregion

    #region Network Status Check
    private void SetupNetworkStatusCheck()
    {
      NetworkChange.NetworkAddressChanged +=
        new NetworkAddressChangedEventHandler
            (NetworkChange_NetworkAddressChanged);
      //Initialize values
      NetworkAvailableTextBlock.Text =
        PhoneNetworkApi.NetworkInterface.GetIsNetworkAvailable().ToString();
      NetworkConnectionTextBlock.Text =
        PhoneNetworkApi.NetworkInterface.NetworkInterfaceType.ToString();
    }

    void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
    {
      NetworkAvailableTextBlock.Text =
        PhoneNetworkApi.NetworkInterface.GetIsNetworkAvailable().ToString();
      NetworkConnectionTextBlock.Text =
        PhoneNetworkApi.NetworkInterface.NetworkInterfaceType.ToString();
    }
    #endregion

    #region License and Trial Info
    private void ApplicationLicenseAndTrialInfo()
    {
      Microsoft.Phone.Marketplace.LicenseInformation licenseInformation = new LicenseInformation();
      IsTrialTextBlock.Text = licenseInformation.IsTrial().ToString();
    }
    #endregion
  }
}