using Microsoft.Phone.Controls;
using AdvancedDataBinding.ViewModel;

namespace AdvancedDataBinding.View
{
  /// <summary>
  /// Description for ShowingProgress.
  /// </summary>
  public partial class ShowingProgress : PhoneApplicationPage
  {
    /// <summary>
    /// Initializes a new instance of the ShowingProgress class.
    /// </summary>
    public ShowingProgress()
    {
      InitializeComponent();
    }

    private void downloadFeed_Click(object sender, System.EventArgs e)
    {
      ShowProgressViewModel vm = this.DataContext as ShowProgressViewModel;
      vm.DownloadAppHubFeed();
    }
  }
}