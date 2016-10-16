/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:PhotosExtraApp.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:PhotosExtraApp.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace PhotosExtraApp.ViewModel
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// <para>
  /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
  /// to this locator.
  /// </para>
  /// <para>
  /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
  /// </para>
  /// <code>
  /// &lt;Application.Resources&gt;
  ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:PhotosExtraApp.ViewModel"
  ///                                  x:Key="Locator" /&gt;
  /// &lt;/Application.Resources&gt;
  /// </code>
  /// <para>
  /// Then use:
  /// </para>
  /// <code>
  /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  /// </code>
  /// <para>
  /// You can also use Blend to do all this with the tool's support.
  /// </para>
  /// <para>
  /// See http://www.galasoft.ch/mvvm/getstarted
  /// </para>
  /// <para>
  /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
  /// the Main property and bind to the ViewModelNameStatic property instead:
  /// </para>
  /// <code>
  /// xmlns:vm="clr-namespace:PhotosExtraApp.ViewModel"
  /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
  /// </code>
  /// </summary>
  public class ViewModelLocator
  {
    private static MainViewModel _main;

    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    public ViewModelLocator()
    {
      ////if (ViewModelBase.IsInDesignModeStatic)
      ////{
      ////    // Create design time view models
      ////}
      ////else
      ////{
      ////    // Create run time view models
      ////}

      CreateMain();
      CreatePictureEditing();
    }


    public static MainViewModel MainStatic
    {
      get
      {
        if (_main == null)
        {
          CreateMain();
        }

        return _main;
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public MainViewModel Main
    {
      get
      {
        return MainStatic;
      }
    }

    public static void ClearMain()
    {
      _main.Cleanup();
      _main = null;
    }

    public static void CreateMain()
    {
      if (_main == null)
      {
        _main = new MainViewModel();
      }
    }

    private static PictureEditingViewModel _pictureEditingStatic;
    public static PictureEditingViewModel PictureEditingStatic
    {
      get
      {
        if (_pictureEditingStatic == null)
        {
          CreatePictureEditing();
        }

        return _pictureEditingStatic;
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public PictureEditingViewModel PictureEditing
    {
      get
      {
        return PictureEditingStatic;
      }
    }

    public static void ClearPictureEditing()
    {
      _pictureEditingStatic.Cleanup();
      _pictureEditingStatic = null;
    }

    public static void CreatePictureEditing()
    {
      if (_pictureEditingStatic == null)
      {
        _pictureEditingStatic = new PictureEditingViewModel();
      }
    }

    /// <summary>
    /// Cleans up all the resources.
    /// </summary>
    public static void Cleanup()
    {
      ClearMain();
      ClearPictureEditing();
    }
  }
}