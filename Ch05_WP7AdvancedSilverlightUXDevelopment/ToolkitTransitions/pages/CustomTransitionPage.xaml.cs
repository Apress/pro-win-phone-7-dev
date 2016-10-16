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
  public partial class CustomTransitionPage : PhoneApplicationPage
  {
    public CustomTransitionPage()
    {
      InitializeComponent();
    }
  }

  public class TheTransition : ITransition
  {
    private Storyboard _storyboard;

    public TheTransition(Storyboard storyBoard)
    {
      _storyboard = storyBoard;
    }

    public void Begin()
    {
      _storyboard.Begin();
    }

    //public event EventHandler Completed;

    public ClockState GetCurrentState()
    {
      return _storyboard.GetCurrentState();
    }

    public TimeSpan GetCurrentTime()
    {
      return _storyboard.GetCurrentTime();
    }

    public void Pause()
    {
      _storyboard.Pause();
    }

    public void Resume()
    {
      _storyboard.Resume();
    }

    public void Seek(TimeSpan offset)
    {
      _storyboard.Seek(offset);
    }

    public void SeekAlignedToLastTick(TimeSpan offset)
    {
      _storyboard.SeekAlignedToLastTick(offset);
    }

    public void SkipToFill()
    {
      _storyboard.SkipToFill();
    }

    public void Stop()
    {
      _storyboard.Stop();
    }

    public event EventHandler Completed;
  }

  public class MyTransition : TransitionElement
  {
    public override ITransition GetTransition(UIElement element)
    {
      Storyboard myStoryboard = App.Current.Resources["CustomPageTransitionStoryboard"] as Storyboard;
      //Make sure Storyboard is stopped before modifying it.
      myStoryboard.Stop();
      Storyboard.SetTarget(myStoryboard, element);

      return new TheTransition(myStoryboard);
    }
  }
}