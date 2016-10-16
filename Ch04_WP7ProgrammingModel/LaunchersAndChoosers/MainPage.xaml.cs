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
//Namespaces added
using Microsoft.Phone.Tasks;
using System.Text;
using System.Windows.Media.Imaging;

namespace LaunchersAndChoosers
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
    }
    #region Email Task
    private void EmailComposeTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      //cause an exception and then send an error report.
      try
      {
        int num1 = 3;
        int num2 = 3;
        int num3 = num1 / (num1 - num2);
      }
      catch (Exception err)
      {
        EmailComposeTask task = new EmailComposeTask();
        task.To = "support@WP7isv.com";
        task.Subject = "Customer Error Report";
        //Size StringBuilder appropriately
        StringBuilder builder;
        if (null == err.InnerException)
          builder = new StringBuilder(600);
        else //need space for InnerException
          builder = new StringBuilder(1200);
        builder.AppendLine("Please tell us what you were doing when the problem occurred.\n\n\n");
        builder.AppendLine("EXCEPTION DETAILS:");
        builder.Append("message:");
        builder.AppendLine(err.Message);
        builder.AppendLine("");
        builder.Append("stack trace:");
        builder.AppendLine(err.StackTrace);
        if (null != err.InnerException)
        {
          builder.AppendLine("");
          builder.AppendLine("");
          builder.AppendLine("inner exception:");
          builder.Append("inner exception message:");
          builder.AppendLine(err.InnerException.Message);
          builder.AppendLine("");
          builder.Append("inner exception stack trace:");
          builder.AppendLine(err.InnerException.StackTrace);
        }
        task.Body = builder.ToString();
        task.Show();
      }
    }
    #endregion

    #region Marketplace Tasks
    private void MarketplaceHubTask1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MarketplaceHubTask marketplaceHubTask = new MarketplaceHubTask();
      marketplaceHubTask.ContentType = MarketplaceContentType.Applications;
      marketplaceHubTask.Show();
    }

    private void MarketplaceHubTask2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MarketplaceHubTask marketplaceHubTask = new MarketplaceHubTask();
      marketplaceHubTask.ContentType = MarketplaceContentType.Music;
      marketplaceHubTask.Show();
    }

    private void MarketplaceSearchTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();
      marketplaceSearchTask.ContentType = MarketplaceContentType.Music;
      marketplaceSearchTask.SearchTerms = "driving music";
      marketplaceSearchTask.Show();
    }

    private void MarketplaceDetailTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
      marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
      //AppID for the youtube application
      marketplaceDetailTask.ContentIdentifier = "dcbb1ac6-a89a-df11-a490-00237de2db9e";
      marketplaceDetailTask.Show();
    }

    private void MarketplaceReviewTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
      marketplaceReviewTask.Show();
    }
    #endregion

    private void MediaPlayerLauncher_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
      mediaPlayerLauncher.Controls = MediaPlaybackControls.FastForward |
        MediaPlaybackControls.Pause | MediaPlaybackControls.Rewind |
        MediaPlaybackControls.Skip | MediaPlaybackControls.Stop;
      mediaPlayerLauncher.Location = MediaLocationType.Data;
      mediaPlayerLauncher.Media = new Uri("http://ecn.channel9.msdn.com/o9/ch9/8/9/6/6/3/5/WP7Xbox_ch9.mp4");
      mediaPlayerLauncher.Show();
    }

    private void PhoneCallTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      PhoneCallTask phoneCallTask = new PhoneCallTask();
      phoneCallTask.DisplayName = "Rob Cameron";
      phoneCallTask.PhoneNumber = "555-555-1111";
      phoneCallTask.Show();
    }

    private void SearchTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      SearchTask searchTask = new SearchTask();
      searchTask.SearchQuery = "driving music";
      searchTask.Show();
    }

    private void SMSComposeTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      Microsoft.Phone.Tasks.SmsComposeTask smsComposeTask = new SmsComposeTask();
      smsComposeTask.To = "555-555-5555";
      smsComposeTask.Body = "Meet me for pizza.";
      smsComposeTask.Show();
    }

    private void WebBrowserTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      WebBrowserTask webBrowserTask = new WebBrowserTask();
      webBrowserTask.URL = "http://create.msdn.com";
      webBrowserTask.Show();
    }

    #region Email Address Chooser
    private void EmailAddressChooserTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      EmailAddressChooserTask emailAddressChooserTask = new EmailAddressChooserTask();
      emailAddressChooserTask.Completed += new EventHandler<EmailResult>(emailAddressChooserTask_Completed);
      emailAddressChooserTask.Show();
    }

    void emailAddressChooserTask_Completed(object sender, EmailResult e)
    {
      if ((null == e.Error) && (TaskResult.OK == e.TaskResult))
      {
        MessageBox.Show("Email address returned is: " + e.Email);
      }
    }
    #endregion

    #region Phone Number  Chooser
    private void PhoneNumberChooserTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      PhoneNumberChooserTask phoneNumberChooserTask = new PhoneNumberChooserTask();
      phoneNumberChooserTask.Completed += new EventHandler<PhoneNumberResult>(phoneNumberChooserTask_Completed);
      phoneNumberChooserTask.Show();
    }

    void phoneNumberChooserTask_Completed(object sender, PhoneNumberResult e)
    {
      if ((null == e.Error) && (TaskResult.OK == e.TaskResult))
      {
        MessageBox.Show("Phone number returned is: " + e.PhoneNumber);
      }
    }
    #endregion

    #region Photo Chooser Task
    private void PhotoChooserTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      PhotoChooserTask photoChoserTask = new PhotoChooserTask();
      photoChoserTask.ShowCamera = true;
      photoChoserTask.Completed += new EventHandler<PhotoResult>(photoChoserTask_Completed);
      photoChoserTask.Show();
    }

    private BitmapImage capturedImage;
    void photoChoserTask_Completed(object sender, PhotoResult e)
    {
      if ((null == e.Error) && (TaskResult.OK == e.TaskResult))
      {
        capturedImage = new BitmapImage();
        capturedImage.SetSource(e.ChosenPhoto);
        ChosenPhotoImage.Source = capturedImage;
      }
    }
    #endregion

    #region Save Email Address Task
    private void SaveEmailAdressTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      SaveEmailAddressTask saveEmailAddressTask = new SaveEmailAddressTask();
      saveEmailAddressTask.Completed += new EventHandler<TaskEventArgs>(saveEmailAddressTask_Completed);
      saveEmailAddressTask.Email = "email@domain.com";
      MessageBox.Show("Saving this email: " + saveEmailAddressTask.Email);
      saveEmailAddressTask.Show();
    }

    void saveEmailAddressTask_Completed(object sender, TaskEventArgs e)
    {
      if ((null == e.Error) && (TaskResult.OK == e.TaskResult))
      {
        MessageBox.Show("Email address saved");
      }
    }
    #endregion

    #region Save Phone Number Task
    private void SavePhoneNumberTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      SavePhoneNumberTask savePhoneNumberTask = new SavePhoneNumberTask();
      savePhoneNumberTask.Completed += new EventHandler<TaskEventArgs>(savePhoneNumberTask_Completed);
      savePhoneNumberTask.PhoneNumber = "555-555-5555";
      MessageBox.Show("Saving this phone number: " + savePhoneNumberTask.PhoneNumber);
      savePhoneNumberTask.Show();
    }

    void savePhoneNumberTask_Completed(object sender, TaskEventArgs e)
    {
      if ((null == e.Error) && (TaskResult.OK == e.TaskResult))
      {
        MessageBox.Show("Phone number saved");
      }
    }
    #endregion
  }
}