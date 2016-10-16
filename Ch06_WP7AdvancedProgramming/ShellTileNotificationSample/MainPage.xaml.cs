using System;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ShellTileNotificationSample
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }
    //store as global variable so that the schedule
    //can be Started and Stopped.
    private ShellTileSchedule shellTileSchedule;
    private void StartShellTileAppBarBtn_Click(object sender, EventArgs e)
    {
      StartShellTileSchedule();
    }

    private void StartShellTileSchedule()
    {
      shellTileSchedule = new ShellTileSchedule();
      shellTileSchedule.Recurrence = UpdateRecurrence.Interval;
      shellTileSchedule.Interval = UpdateInterval.EveryHour;
      shellTileSchedule.StartTime = DateTime.Now;
      shellTileSchedule.RemoteImageUri =
        new Uri(@"http://apress.com/resource/bookcover/9781430232193?size=medium");
      shellTileSchedule.Start();
    }

    private void StopShellTileAppBarBtn_Click(object sender, EventArgs e)
    {
      if (shellTileSchedule != null)
        shellTileSchedule.Stop();
    }
  }
}