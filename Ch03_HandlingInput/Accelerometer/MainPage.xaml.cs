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
using Microsoft.Devices.Sensors;

namespace AccelerometerInput
{
  public partial class MainPage : PhoneApplicationPage
  {
    private Accelerometer accelerometerSensor;
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      accelerometerSensor = new Accelerometer();
      accelerometerSensor.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(accelerometerSensor_ReadingChanged);
    }

    void accelerometerSensor_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
    {
      Deployment.Current.Dispatcher.BeginInvoke(() => AccelerometerReadingChanged(e)); 
    }

    private void AccelerometerReadingChanged(AccelerometerReadingEventArgs e)
    {
      xAxis.Text = String.Format("{0:0.00}", e.X);
      yAxis.Text = String.Format("{0:0.00}", e.Y);
      zAxis.Text = String.Format("{0:0.00}", e.Z);
    }

    private void StartAccelerometerButton_Click(object sender, EventArgs e)
    {
      accelerometerSensor.Start();
    }

    private void StopAccelerometerButton_Click(object sender, EventArgs e)
    {
      accelerometerSensor.Stop();
    }
  }
}