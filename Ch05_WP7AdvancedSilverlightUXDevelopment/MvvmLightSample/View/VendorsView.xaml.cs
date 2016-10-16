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
using MvvmLightSample.ViewModel;
using System.Windows.Threading;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;

namespace MvvmLightSample.View
{
  public partial class VendorsView : PhoneApplicationPage
  {
    public VendorsView()
    {
      InitializeComponent();
    }

    private void insertVendorAppBarBtn_Click(object sender, EventArgs e)
    {
      var vm = DataContext as VendorViewModel;
      if (vm != null)
      {
        vm.AddAVendorCommand.Execute(null);
      }
    }

    private void RemoveVendorAppBarBtn_Click(object sender, EventArgs e)
    {
      var vm = DataContext as VendorViewModel;
      if (vm != null)
      {
        vm.RemoveAVendorCommand.Execute(
          vendorsListBox.SelectedItem);
      }
    }    
  }
}