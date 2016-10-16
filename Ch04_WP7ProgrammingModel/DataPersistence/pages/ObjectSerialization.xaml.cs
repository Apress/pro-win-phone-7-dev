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
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;


namespace DataPersistence.pages
{
  public partial class ObjectSerialization : PhoneApplicationPage
  {
    private const string fileName = "AppClass.dat";

    public ObjectSerialization()
    {
      InitializeComponent();
      LayoutRoot.DataContext = new AppClass();
      
    }

    private void saveAppBarIcon_Click(object sender, EventArgs e)
    {
      SaveData();
    }

    private void loadAppBarIcon_Click(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (isf.FileExists(fileName))
        {
          using (IsolatedStorageFileStream fs =
            isf.OpenFile(fileName, System.IO.FileMode.Open))
          {
            XmlSerializer serializer = new XmlSerializer(typeof(AppClass));
            LayoutRoot.DataContext = (AppClass)serializer.Deserialize(fs);
          }
        }
      }
    }

    private void SaveData()
    {
      using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
      {
        using (IsolatedStorageFileStream fs =
          isf.OpenFile(fileName, System.IO.FileMode.Create))
        {
          XmlSerializer xs = new XmlSerializer(typeof(AppClass));
          xs.Serialize(fs, ((AppClass)LayoutRoot.DataContext));
        }
      }
    }

  }
}