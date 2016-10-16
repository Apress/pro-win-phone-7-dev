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

namespace SinglePointTouch.Pages
{
  public partial class TextBoxMouseEventPage : PhoneApplicationPage
  {
    public TextBoxMouseEventPage()
    {
      InitializeComponent();
    }


    private void MouseEventsTextBox_MouseEnter(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseEnter event fired.");
    }

    private void MouseEventsTextBox_MouseLeave(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseLeave event fired.");
    }

    private void MouseEventsTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseLeftButtonDown event fired.");
    }

    private void MouseEventsTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseLeftButtonUp event fired.");
    }

    private void MouseEventsTextBox_MouseMove(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseMove event fired.");
    }

    private void MouseEventsTextBox_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      MouseEventLogListBox.Items.Add("MouseWheel event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseEnter(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseEnter event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseLeave(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseLeave event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseLeftButtonDown event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseLeftButtonUp event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseMove(object sender, MouseEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseMove event fired.");
    }

    private void TextBlockMouseEventsDemo_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      MouseEventLogListBox2.Items.Add("MouseWheel event fired.");
    }
  }
}