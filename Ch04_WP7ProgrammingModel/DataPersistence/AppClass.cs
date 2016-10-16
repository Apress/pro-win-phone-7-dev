using System.Xml.Serialization;

namespace DataPersistence 
{
  [XmlRootAttribute("AppClass")]
  public class AppClass
  {
    public AppClass()
    {
      FavoriteColor = -1;
    }

    //Settings
    [XmlElement]
    public bool EnablePushNotifications { get; set; }

    [XmlElement]
    public int FavoriteColor { get; set; }

    [XmlElement]
    public string NickName { get; set; }

    //Data
    [XmlElement]
    public string Notes { get; set; }
  }
}
