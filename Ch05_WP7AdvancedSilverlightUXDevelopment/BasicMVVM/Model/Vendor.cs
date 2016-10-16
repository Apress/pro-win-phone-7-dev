using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace BasicMVVM.Models
{
  //Copied from services project
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