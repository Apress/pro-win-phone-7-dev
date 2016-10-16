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
//XNA Namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace GesturesTouchPanelSilverlight
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      TouchPanel.EnabledGestures = GestureType.DoubleTap | GestureType.Flick |
        GestureType.FreeDrag | GestureType.Hold | GestureType.HorizontalDrag |
        GestureType.None | GestureType.Pinch | GestureType.PinchComplete |
        GestureType.Tap | GestureType.VerticalDrag | GestureType.DragComplete;
    }

    private void PhoneApplicationPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      while (TouchPanel.IsGestureAvailable)
      {
        GestureActionsListBox.Items.Add("LeftBtnDown "+TouchPanel.ReadGesture().GestureType.ToString());

      }
    }

    private void PhoneApplicationPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      while (TouchPanel.IsGestureAvailable)
      {
        GestureActionsListBox.Items.Add("LeftBtnUp " + TouchPanel.ReadGesture().GestureType.ToString());
      }
    }

    private void PhoneApplicationPage_MouseMove(object sender, MouseEventArgs e)
    {
      while (TouchPanel.IsGestureAvailable)
      {
        GestureActionsListBox.Items.Add("MouseMove " + TouchPanel.ReadGesture().GestureType.ToString());
      }
    }
  }
}