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

namespace GesturesSilverlightToolkit
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void GestureListener_DoubleTap(object sender, GestureEventArgs e)
    {
      ReportGesture("DoubeTap");
    }

    private void GestureListener_DragCompleted(object sender, DragCompletedGestureEventArgs e)
    {
      ReportGesture("Drag Completed");
    }

    private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
    {
      ReportGesture("Drag Delta");
    }

    private void GestureListener_DragStarted(object sender, DragStartedGestureEventArgs e)
    {
      ReportGesture("Drag Started");
    }

    private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
    {
      ReportGesture("Flick");
    }

    private void GestureListener_GestureBegin(object sender, GestureEventArgs e)
    {
      GestureListBox.Items.Clear();
      ReportGesture("Gesture Begin");
    }

    private void GestureListener_GestureCompleted(object sender, GestureEventArgs e)
    {
      ReportGesture("Gesture Completed");
    }

    private void GestureListener_Hold(object sender, GestureEventArgs e)
    {
      ReportGesture("Hold");
    }

    private void GestureListener_PinchStarted(object sender, PinchStartedGestureEventArgs e)
    {
      ReportGesture("Pinch Started");
    }

    private void GestureListener_PinchDelta(object sender, PinchGestureEventArgs e)
    {
      ReportGesture("Pinch Delta");
    }

    private void GestureListener_PinchCompleted(object sender, PinchGestureEventArgs e)
    {
      ReportGesture("Pinch Completed");
    }

    private void GestureListener_Tap(object sender, GestureEventArgs e)
    {
      ReportGesture("Tap");
    }

    private void ReportGesture(string p)
    {
      GestureListBox.Items.Add(p);
    }
  }
}