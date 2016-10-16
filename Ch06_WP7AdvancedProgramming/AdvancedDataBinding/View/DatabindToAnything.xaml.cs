using Microsoft.Phone.Controls;
using AdvancedDataBinding.ViewModel;

namespace AdvancedDataBinding.View
{
  /// <summary>
  /// Description for DatabindToAnything.
  /// </summary>
  public partial class DatabindToAnything : PhoneApplicationPage
  {
    /// <summary>
    /// Initializes a new instance of the DatabindToAnything class.
    /// </summary>
    public DatabindToAnything()
    {
      InitializeComponent();
    }

    private void DownloadAppHubFeedAppBarBtn_Click(object sender, System.EventArgs e)
    {
      DataConverterViewModel vm = this.DataContext as DataConverterViewModel;
      vm.DownloadAppHubFeed();
    }
  }
}