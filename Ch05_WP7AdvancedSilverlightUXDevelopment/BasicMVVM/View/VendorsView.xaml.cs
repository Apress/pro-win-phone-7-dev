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
using BasicMVVM.ViewModels;
using System.Windows.Threading;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;

namespace BasicMVVM.View
{
  public partial class VendorsView : PhoneApplicationPage
  {
    public VendorsView()
    {
      InitializeComponent();
    }

    private void insertVendorAppBarBtn_Click(object sender, EventArgs e)
    {
      VendorViewModel vm = LayoutRoot.DataContext as VendorViewModel;
      vm.AddVendor();
    }

    private void RemoveVendorAppBarBtn_Click(object sender, EventArgs e)
    {
      VendorViewModel vm = LayoutRoot.DataContext as VendorViewModel;
      vm.RemoveVendor(vendorsListBox.SelectedItem);
    }    
  }
}