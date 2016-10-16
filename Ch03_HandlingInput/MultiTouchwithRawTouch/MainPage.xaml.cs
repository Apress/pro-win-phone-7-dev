using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MultiTouchwithRawTouch
{
  public partial class MainPage : PhoneApplicationPage
  {
    List<TrackedTouchPoint> trackedTouchPoints = new List<TrackedTouchPoint>();

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
    }

    void Touch_FrameReported(object sender, TouchFrameEventArgs e)
    {
      foreach (TouchPoint tp in e.GetTouchPoints(DrawCanvas))
      {
        TrackedTouchPoint ttp = null;
        var query = from point in trackedTouchPoints
                    where point.ID == tp.TouchDevice.Id
                    select point;
        if (query.Count() != 0)
          ttp = query.First();

        switch (tp.Action)
        {
          case TouchAction.Down: ttp = new TrackedTouchPoint();
            ttp.ID = tp.TouchDevice.Id;
            if (trackedTouchPoints.Count == 0)
            {
              ttp.IsPrimary = true;
              DrawCanvas.Children.Clear();
            }
            trackedTouchPoints.Add(ttp);
            ttp.Position = tp.Position;
            ttp.Draw(DrawCanvas);
            break;

          case TouchAction.Up: ttp.UnDraw(DrawCanvas);
            trackedTouchPoints.Remove(ttp);
            break;
          default:
            ttp.Position = tp.Position;
            ttp.Draw(DrawCanvas);
            break;
        }
      }
      CleanUp(e.GetTouchPoints(DrawCanvas));
    }

    private void CleanUp(TouchPointCollection tpc)
    {
      List<int> ToDelete = new List<int>();
      foreach (TrackedTouchPoint ttp in trackedTouchPoints)
      {
        var query = from point in tpc
                    where point.TouchDevice.Id == ttp.ID
                    select point;
        if (query.Count() == 0)
          ToDelete.Add(ttp.ID);
      }

      foreach (int i in ToDelete)
      {
        var query = from point in trackedTouchPoints
                    where point.ID == i
                    select point;
        if (query.Count() != 0)
          trackedTouchPoints.Remove(query.First());
      }
      if (trackedTouchPoints.Count == 0)
      {
        DrawCanvas.Children.Clear();
      }
    }
  }

  class TrackedTouchPoint
  {
    public TrackedTouchPoint()
    {
      Rect = new Rectangle() { Height = 50, Width = 50 };
      Position = new Point(0, 0);
      IsPrimary = false;
      BrushColor = new SolidColorBrush(Colors.Yellow);
    }

    private Rectangle Rect { get; set; }

    public int ID { get; set; }

    public Brush BrushColor
    {
      set
      {
        Rect.Fill = value;
      }
    }
    public Point Position { get; set; }

    public bool IsPrimary { get; set; }

    public void Draw(Canvas canvas)
    {
      if (IsPrimary)
        BrushColor = new SolidColorBrush(Colors.Blue);

      Rect.SetValue(Canvas.LeftProperty, Position.X);
      Rect.SetValue(Canvas.TopProperty, Position.Y);
      if (Rect.Parent == null)
        canvas.Children.Add(Rect);
    }

    public void UnDraw(Canvas canvas)
    {
      canvas.Children.Remove(Rect);
    }
  }
}