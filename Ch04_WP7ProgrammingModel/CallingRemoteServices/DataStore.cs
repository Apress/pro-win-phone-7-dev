using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RestJSON.DataModel
{
  sealed public class DataStore
  {
    //Declare suported collection types here
    public ObservableCollection<Vendor> Vendors { get; set; }

    //Provide a static read-only instance
    private static readonly DataStore instance = new DataStore();

    //Private Constructor for Singleton
    public DataStore() { }

    //The entry point into this Database 
    public static DataStore Instance
    {
      get
      {
        return instance;
      }
    }

    //Deserialize ObservableCollection from JSON
    public T LoadCollection<T>(T collectionToLoad, string collectionName)
    {
      using (IsolatedStorageFile store =
              IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (store.FileExists(collectionName + ".txt"))
        {
          using (IsolatedStorageFileStream stream = store.OpenFile(
            collectionName + ".txt", System.IO.FileMode.Open))
          {
            DataContractJsonSerializer serializer =
              new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
          }
        }
        else
        {
          throw new Exception("Table not found");
        }
      }
    }

    //Serialize ObservableCollection to JSON
    public void SaveCollection<T>(T collectionToSave, string collectionName)
    {
      if (collectionToSave != null)
      {
        using (IsolatedStorageFile store = 
                IsolatedStorageFile.GetUserStoreForApplication())
        {
          using (IsolatedStorageFileStream stream = 
            store.CreateFile(collectionName + ".txt"))
          {
            DataContractJsonSerializer serializer = 
              new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, collectionToSave);
          }
        }
      }
    }

    //Delete ObservableCollection from Iso Storage 
    public void DeleteCollection(string tableName)
    {
      using (IsolatedStorageFile store = 
             IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (store.FileExists(tableName + ".txt"))
        {
          store.DeleteFile(tableName + ".txt");
        }
      }
    }
  }

  [DataContract()]
  public class Vendor : INotifyPropertyChanged
  {
    private string AccountNumberField;
    private byte CreditRatingField;
    private string NameField;

    [DataMemberAttribute()]
    public string AccountNumber
    {
      get
      {
        return this.AccountNumberField;
      }
      set
      {
        this.AccountNumberField = value;
        NotifyPropertyChanged("AccountNumber");
      }
    }

    [DataMemberAttribute()]
    public byte CreditRating
    {
      get
      {
        return this.CreditRatingField;
      }
      set
      {
        this.CreditRatingField = value;
        NotifyPropertyChanged("CreditRating");
      }
    }

    [DataMemberAttribute()]
    public string Name
    {
      get
      {
        return this.NameField;
      }
      set
      {
        this.NameField = value;
        NotifyPropertyChanged("Name");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
      if (null != PropertyChanged)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}



