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

namespace ToolkitTransitions.pages
{
  public partial class TestTransitionsPage : PhoneApplicationPage
  {
    public TestTransitionsPage()
    {
      InitializeComponent();
    }

    private void sampleDataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (sampleDataListBox.SelectedIndex == -1)
        return;

      // Navigate to the new page
      NavigationService.Navigate(new Uri("/pages/TestTransitionsPage2.xaml?selectedItem=" + sampleDataListBox.SelectedIndex, UriKind.Relative));

      // Reset selected index to -1 (no selection)
      sampleDataListBox.SelectedIndex = -1;
    }
  }
}