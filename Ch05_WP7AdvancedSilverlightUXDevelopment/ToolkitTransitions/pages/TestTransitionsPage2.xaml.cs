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
using Expression.Blend.SampleData.SampleDataSource;
namespace ToolkitTransitions.pages
{
  public partial class TurnstyleNavigationPage : PhoneApplicationPage
  {
    public TurnstyleNavigationPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      string selectedIndex = "";
      if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
      {
        int index = int.Parse(selectedIndex);
        ContentPanel.DataContext = ((SampleDataSource)LayoutRoot.DataContext).Collection[index];
      }

      base.OnNavigatedTo(e);
    }
  }
}