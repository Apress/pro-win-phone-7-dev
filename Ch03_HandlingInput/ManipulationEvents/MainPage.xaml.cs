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

namespace ManipulationEvents
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }

    private void ReportEvent(string p)
    {
      ManipulationEventsListBox.Items.Add(p);
    }

    private void StickManImage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      ReportEvent("Manipulation Completed Event: ");
    }

    private void StickManImage_ManipulationDelta(object sender,
      ManipulationDeltaEventArgs e)
    {
      ReportEvent("Manipulation Delta Event: ");
      Image image = sender as Image;
      CompositeTransform compositeTransform =
        image.RenderTransform as CompositeTransform;

      if ((e.DeltaManipulation.Scale.X > 0) || (e.DeltaManipulation.Scale.Y > 0))
      {
        double ScaleValue = Math.Max(e.DeltaManipulation.Scale.X,
          e.DeltaManipulation.Scale.Y);
        System.Diagnostics.Debug.WriteLine("Scale Value: " + 
          ScaleValue.ToString());

        //Limit how large 
        if ((compositeTransform.ScaleX <= 4d) || (ScaleValue < 1d))
        {
          compositeTransform.ScaleX *= ScaleValue;
          compositeTransform.ScaleY *= ScaleValue;
        }
      }
      System.Diagnostics.Debug.WriteLine("compositeTransform.ScaleX: " + 
        compositeTransform.ScaleX);
      System.Diagnostics.Debug.WriteLine("compositeTransform.ScaleY: " + 
        compositeTransform.ScaleY);

      compositeTransform.TranslateX += e.DeltaManipulation.Translation.X;
      compositeTransform.TranslateY += e.DeltaManipulation.Translation.Y;
      e.Handled = true;
    }

    private void StickManImage_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      ReportEvent("Manipulation Started Event: ");
    }
  }
}