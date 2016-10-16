using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using ConfigSettingsDemo;

namespace ConfigSettingsDemo.Services
{
  public class ConfigurationSettingsService : IApplicationService
  {
    private string _configSettingsPath = @"\Settings\";
    private string _configSettingsFileName = "ConfigurationSettings.xml";

    //Event to allow the Application object know it is safe to 
    //access the settings
    public event EventHandler ConfigurationSettingsLoaded;

    #region IApplicationService Members
    void IApplicationService.StartService(ApplicationServiceContext context)
    {
      LoadConfigSettings();
    }

    private void LoadConfigSettings()
    {
      //TODO - Load ConfigSettings from isolated storage
      //using (Stream s = e.Result)
      //{
      //  XDocument xDoc = XDocument.Load(s);
      //  ConfigSettings =
      //  (from setting in xDoc.Descendants("setting")
      //   select setting).ToDictionary(n => n.Element("key").Value, n => n.Element("value").Value);

      //  //Check to see if the event has any handler's attached
      //  //Fire event if that is the case
      //  if (ConfigurationSettingsLoaded != null)
      //    ConfigurationSettingsLoaded(this, EventArgs.Empty);
      //}
      
      //Check to see if the event has any handler's attached
      //Fire event if that is the case
      if (ConfigurationSettingsLoaded != null)
        ConfigurationSettingsLoaded(this, EventArgs.Empty);
    }

    private void SaveConfigSettings()
    {
      //TODO - Save ConfigSettings to isolated storage
    }

    void IApplicationService.StopService()
    {
      SaveConfigSettings();
    }
    #endregion

    //Stores configuraiton settings in 
    public Dictionary<string, string> ConfigSettings { get; set; }
  }
}