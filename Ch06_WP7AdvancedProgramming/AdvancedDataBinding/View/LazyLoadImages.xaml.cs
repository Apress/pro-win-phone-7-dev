using Microsoft.Phone.Controls;
using AdvancedDataBinding.ViewModel;

namespace AdvancedDataBinding.View
{
  /// <summary>
  /// Description for LazyLoadImages.
  /// </summary>
  public partial class LazyLoadImages : PhoneApplicationPage
  {
    /// <summary>
    /// Initializes a new instance of the LazyLoadImages class.
    /// </summary>
    public LazyLoadImages()
    {
      InitializeComponent();
    }

    private void dowloadTopTitlesAppBarBtn_Click(object sender, System.EventArgs e)
    {
      LazyLoadViewModel vm = this.DataContext as LazyLoadViewModel;
      vm.DownloadNetflixTopTitles();
    }
  }
}