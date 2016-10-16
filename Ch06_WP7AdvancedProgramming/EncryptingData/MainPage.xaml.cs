using System.Windows;
using System.Windows.Controls;
using EncryptingData.ViewModel;
using Microsoft.Phone.Controls;

namespace EncryptingData
{
  public partial class MainPage : PhoneApplicationPage
  {
    MainViewModel vm;
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      vm = this.DataContext as MainViewModel;
    }

    private void EncryptAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.EncryptData();
    }

    private void DecryptAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.DecryptData();
    }

    private void SaveIsoAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.SaveEncryptedDataToIsolatedStorage();
    }

    private void LoadIsoAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.LoadEncryptedDataFromIsolatedStorage();
    }

    private void PasswordBox_LostFocus(object sender, 
      System.Windows.RoutedEventArgs e)
    {
      if (((PasswordBox)sender).Password.Length < 8)
        MessageBox.Show("Salt Value must be at least eight characters.",
          "Minimum Length Error",MessageBoxButton.OK);
    }
  }
}
