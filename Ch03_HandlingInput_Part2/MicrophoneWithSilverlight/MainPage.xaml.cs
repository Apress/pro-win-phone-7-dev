using System;
using System.IO;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;

namespace MicrophoneWithSilverlight
{
  public partial class MainPage : PhoneApplicationPage
  {
    Microphone microphone = Microphone.Default;
    MemoryStream audioStream;

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      microphone.BufferReady += 
        new EventHandler<EventArgs>(microphone_BufferReady);
      SoundEffect.MasterVolume = 1.0f;

      MicrophoneStatus.Text = microphone.State.ToString();
    }

    void microphone_BufferReady(object sender, EventArgs e)
    {
      byte[] audioBuffer = new byte[1024];
      int bytesRead = 0;

      while ((bytesRead = microphone.GetData(audioBuffer, 0, audioBuffer.Length)) > 0)
        audioStream.Write(audioBuffer, 0, bytesRead);

      MicrophoneStatus.Text = microphone.State.ToString();
    }

    private void recordButton_Click(object sender, RoutedEventArgs e)
    {
      if (microphone != null)
        microphone.Stop();

      audioStream = new MemoryStream();

      microphone.Start();
      MicrophoneStatus.Text = microphone.State.ToString();
    }

    private void stopRecordingButton_Click(object sender, RoutedEventArgs e)
    {
      if (microphone.State != MicrophoneState.Stopped)
        microphone.Stop();

      audioStream.Position = 0;
      MicrophoneStatus.Text = microphone.State.ToString();
    }

    private void playButton_Click(object sender, RoutedEventArgs e)
    {
      SoundEffect recordedAudio =
        new SoundEffect(audioStream.ToArray(), microphone.SampleRate,
          AudioChannels.Mono);

      recordedAudio.Play(1f, (float)pitchSlider.Value, 0f);
    }
  }
}