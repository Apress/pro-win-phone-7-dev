/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:AdvancedDataBinding.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:AdvancedDataBinding.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace AdvancedDataBinding.ViewModel
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
  ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:AdvancedDataBinding.ViewModel"
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
  /// xmlns:vm="clr-namespace:AdvancedDataBinding.ViewModel"
  /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
  /// </code>
  /// </summary>
  public class ViewModelLocator
  {

    #region SyndicatedServicesViewModel
    private static SyndicatedServicesViewModel _syndicatedServices;
    public static SyndicatedServicesViewModel SyndicatedServicesStatic
    {
      get
      {
        if (_syndicatedServices == null)
        {
          CreateSyndicatedServices();
        }

        return _syndicatedServices;
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public SyndicatedServicesViewModel SyndicatedServices
    {
      get
      {
        return SyndicatedServicesStatic;
      }
    }

    public static void ClearSyndicatedServices()
    {
      _syndicatedServices.Cleanup();
      _syndicatedServices = null;
    }

    public static void CreateSyndicatedServices()
    {
      if (_syndicatedServices == null)
      {
        _syndicatedServices = new SyndicatedServicesViewModel();
      }
    }
    #endregion

    #region ShowProgressViewModel
    private static ShowProgressViewModel _showProgressViewModelPropertyName;
    public static ShowProgressViewModel ShowProgressStatic
    {
      get
      {
        if (_showProgressViewModelPropertyName == null)
        {
          CreateShowProgress();
        }

        return _showProgressViewModelPropertyName;
      }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public ShowProgressViewModel ShowProgress
    {
      get
      {
        return ShowProgressStatic;
      }
    }

    public static void ClearShowProgress()
    {
      _showProgressViewModelPropertyName.Cleanup();
      _showProgressViewModelPropertyName = null;
    }

    public static void CreateShowProgress()
    {
      if (_showProgressViewModelPropertyName == null)
      {
        _showProgressViewModelPropertyName = new ShowProgressViewModel();
      }
    }
    #endregion

    #region MainViewModel
    private static MainViewModel _main;
    /// <summary>
    /// Gets the Main property.
    /// </summary>
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

    /// <summary>
    /// Gets the Main property.
    /// </summary>
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

    /// <summary>
    /// Provides a deterministic way to delete the Main property.
    /// </summary>
    public static void ClearMain()
    {
      _main.Cleanup();
      _main = null;
    }

    /// <summary>
    /// Provides a deterministic way to create the Main property.
    /// </summary>
    public static void CreateMain()
    {
      if (_main == null)
      {
        _main = new MainViewModel();
      }
    }
#endregion

    #region LazyLoadViewModel
    private static LazyLoadViewModel _lazyLoad;
    public static LazyLoadViewModel LazyLoadStatic
    {
      get
      {
        if (_lazyLoad == null)
        {
          CreateLazyLoad();
        }

        return _lazyLoad;
      }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public LazyLoadViewModel LazyLoad
    {
      get
      {
        return LazyLoadStatic;
      }
    }

    public static void ClearLazyLoad()
    {
      _lazyLoad.Cleanup();
      _lazyLoad = null;
    }

    public static void CreateLazyLoad()
    {
      if (_lazyLoad == null)
      {
        _lazyLoad = new LazyLoadViewModel();
      }
    }
    #endregion

    #region Databind to Anything
    private static DataConverterViewModel _dataConverter;
    public static DataConverterViewModel DataConverterStatic
    {
      get
      {
        if (_dataConverter == null)
        {
          CreateDataConverter();
        }

        return _dataConverter;
      }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public DataConverterViewModel DataConverter
    {
      get
      {
        return DataConverterStatic;
      }
    }

    public static void ClearDataConverter()
    {
      _dataConverter.Cleanup();
      _dataConverter = null;
    }

    /// <summary>
    /// Provides a deterministic way to create the DataConverter property.
    /// </summary>
    public static void CreateDataConverter()
    {
      if (_dataConverter == null)
      {
        _dataConverter = new DataConverterViewModel();
      }
    }

    #endregion

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
      CreateSyndicatedServices();
      CreateShowProgress();
      CreateLazyLoad();
      CreateDataConverter();
    }

    /// <summary>
    /// Cleans up all the resources.
    /// </summary>
    public static void Cleanup()
    {
      ClearMain();
      ClearSyndicatedServices();
      ClearShowProgress();
      ClearLazyLoad();
      ClearDataConverter();
    }
  }
}