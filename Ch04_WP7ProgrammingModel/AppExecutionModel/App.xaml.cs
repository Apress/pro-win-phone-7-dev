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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace AppExecutionModel
{
  public partial class App : Application
  {
    string randomState;
    /// <summary>
    /// Provides easy access to the root frame of the Phone Application.
    /// </summary>
    /// <returns>The root frame of the Phone Application.</returns>
    public PhoneApplicationFrame RootFrame { get; private set; }

    /// <summary>
    /// Constructor for the Application object.
    /// </summary>
    public App()
    {
      // Global handler for uncaught exceptions. 
      UnhandledException += Application_UnhandledException;

      // Show graphics profiling information while debugging.
      if (System.Diagnostics.Debugger.IsAttached)
      {
        // Display the current frame rate counters.
        Application.Current.Host.Settings.EnableFrameRateCounter = true;

        // Show the areas of the app that are being redrawn in each frame.
        //Application.Current.Host.Settings.EnableRedrawRegions = true;

        // Enable non-production analysis visualization mode, 
        // which shows areas of a page that are being GPU accelerated with a colored overlay.
        //Application.Current.Host.Settings.EnableCacheVisualization = true;
      }

      // Standard Silverlight initialization
      InitializeComponent();

      // Phone-specific initialization
      InitializePhoneApplication();

      if (PhoneApplicationService.Current.StartupMode ==
          StartupMode.Launch)
      {
        Debug.WriteLine("In App constructor");
        randomState = DateTime.Now.ToString();
        Debug.WriteLine("Random State: = " + randomState);
      }
    }

    // Code to execute when the application is launching (eg, from Start)
    // This code will not execute when the application is reactivated
    private void Application_Launching(object sender, LaunchingEventArgs e)
    {
      Debug.WriteLine("In Application_Launching");
    }

    // Code to execute when the application is activated (brought to foreground)
    // This code will not execute when the application is first launched
    private void Application_Activated(object sender, ActivatedEventArgs e)
    {
      Debug.WriteLine("Activating - load state");
      IDictionary<string, object> state = 
        PhoneApplicationService.Current.State;
      if (state.ContainsKey("Random State"))
      {
        randomState = state["Random State"] as String;
      }
      Debug.WriteLine("Random State Restore - " + randomState);
      Debug.WriteLine("In Application_Activated");
    }

    // Code to execute when the application is deactivated (sent to background)
    // This code will not execute when the application is closing
    private void Application_Deactivated(object sender, DeactivatedEventArgs e)
    {
      Debug.WriteLine("Deactivating - save state");
      IDictionary<string, object> state =
        PhoneApplicationService.Current.State;
      state["Random State"] = randomState;
      Debug.WriteLine("In Application_Deactivated");
    }

    // Code to execute when the application is closing (eg, user hit Back)
    // This code will not execute when the application is deactivated
    private void Application_Closing(object sender, ClosingEventArgs e)
    {
      Debug.WriteLine("In Application_Closing");
    }

    // Code to execute if a navigation fails
    private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      if (System.Diagnostics.Debugger.IsAttached)
      {
        // A navigation has failed; break into the debugger
        System.Diagnostics.Debugger.Break();
      }
    }

    // Code to execute on Unhandled Exceptions
    private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      if (System.Diagnostics.Debugger.IsAttached)
      {
        // An unhandled exception has occurred; break into the debugger
        System.Diagnostics.Debugger.Break();
      }
    }

    #region Phone application initialization

    // Avoid double-initialization
    private bool phoneApplicationInitialized = false;

    // Do not add any additional code to this method
    private void InitializePhoneApplication()
    {
      if (phoneApplicationInitialized)
        return;

      // Create the frame but don't set it as RootVisual yet; this allows the splash
      // screen to remain active until the application is ready to render.
      RootFrame = new PhoneApplicationFrame();
      RootFrame.Navigated += CompleteInitializePhoneApplication;

      // Handle navigation failures
      RootFrame.NavigationFailed += RootFrame_NavigationFailed;

      // Ensure we don't initialize again
      phoneApplicationInitialized = true;
    }

    // Do not add any additional code to this method
    private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
    {
      // Set the root visual to allow the application to render
      if (RootVisual != RootFrame)
        RootVisual = RootFrame;

      // Remove this handler since it is no longer needed
      RootFrame.Navigated -= CompleteInitializePhoneApplication;
    }

    #endregion

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      Debug.WriteLine("In Application_Startup");
    }

    private void Application_Exit(object sender, EventArgs e)
    {
      Debug.WriteLine("In Application_Exit");
    }
  }
}