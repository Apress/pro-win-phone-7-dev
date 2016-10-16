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

namespace PageNavigation
{
  public partial class Page3 : PhoneApplicationPage
  {
    public Page3()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      if (NavigationContext.QueryString.ContainsKey("page3data"))
        dataFromPage2TextBlock.Text = NavigationContext.QueryString["page3data"];

      base.OnNavigatedTo(e);
    }
  }
}