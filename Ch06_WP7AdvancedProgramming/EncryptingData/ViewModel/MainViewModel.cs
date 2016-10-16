using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;

namespace EncryptingData.ViewModel
{
  /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
  public class MainViewModel : ViewModelBase
  {
    public string ApplicationTitle
    {
      get
      {
        return "CHAPTER SIX";
      }
    }

    public string PageName
    {
      get
      {
        return "encrypting data";
      }
    }

    public const string DataToEncryptPropertyName = "DataToEncrypt";
    private string _dataToEncrypt = null;
    public string DataToEncrypt
    {
      get
      {
        return _dataToEncrypt;
      }

      set
      {
        if (_dataToEncrypt == value)
        {
          return;
        }

        var oldValue = _dataToEncrypt;
        _dataToEncrypt = value;
        RaisePropertyChanged(DataToEncryptPropertyName);
      }
    }


    public const string StorageKeyNamePropertyName = "StorageKeyName";
    private string _storageKeyName = "Passwordkey";
    public string StorageKeyName
    {
      get
      {
        return _storageKeyName;
      }

      set
      {
        if (_storageKeyName == value)
        {
          return;
        }
        var oldValue = _storageKeyName;
        _storageKeyName = value;
        RaisePropertyChanged(StorageKeyNamePropertyName);
      }
    }

    public const string PasswordPropertyName = "Password";
    private string _password = null;
    public string Password
    {
      get
      {
        return _password;
      }

      set
      {
        if (_password == value)
        {
          return;
        }

        var oldValue = _password;
        _password = value;
        RaisePropertyChanged(PasswordPropertyName);
      }
    }

    public const string SaltValuePropertyName = "SaltValue";
    private string _saltValue = null;
    public string SaltValue
    {
      get
      {
        return _saltValue;
      }

      set
      {
        if (_saltValue == value)
        {
          return;
        }

        var oldValue = _saltValue;
        _saltValue = value;
        RaisePropertyChanged(SaltValuePropertyName);
      }
    }

    public const string EncryptedDataPropertyName = "EncryptedData";
    private string _encryptedData = null;
    public string EncryptedData
    {
      get
      {
        return _encryptedData;
      }

      set
      {
        if (_encryptedData == value)
        {
          return;
        }

        var oldValue = _encryptedData;
        _encryptedData = value;
        RaisePropertyChanged(EncryptedDataPropertyName);
      }
    }

    public const string DecryptedDataPropertyName = "DecryptedData";
    private string _decryptedData = null;
    public string DecryptedData
    {
      get
      {
        return _decryptedData;
      }

      set
      {
        if (_decryptedData == value)
        {
          return;
        }

        var oldValue = _decryptedData;
        _decryptedData = value;
        RaisePropertyChanged(DecryptedDataPropertyName);
      }
    }

    public void EncryptData()
    {
      using (AesManaged aes = new AesManaged())
      {
        Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(Password,
          Encoding.UTF8.GetBytes(SaltValue), 10000);
        aes.Key = rfc2898.GetBytes(32);
        aes.IV = rfc2898.GetBytes(16);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream(memoryStream,
            aes.CreateEncryptor(), CryptoStreamMode.Write))
          {
            //Encrypt Data with created CryptoStream
            byte[] secret = Encoding.UTF8.GetBytes(DataToEncrypt);
            cryptoStream.Write(secret, 0, secret.Length);
            cryptoStream.FlushFinalBlock();
            aes.Clear();
            //Set values on UI thread 
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
              EncryptedData = Convert.ToBase64String(memoryStream.ToArray());
            });
          }
        }
      }
    }

    public void DecryptData()
    {
      MemoryStream memoryStream = null;

      using (AesManaged aes = new AesManaged())
      {
        //Generate Key and IV values for decryption
        Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(Password, Encoding.UTF8.GetBytes(SaltValue), 10000);
        aes.Key = rfc2898.GetBytes(32);
        aes.IV = rfc2898.GetBytes(16);

        using (memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = 
            new CryptoStream(memoryStream, aes.CreateDecryptor(), 
                             CryptoStreamMode.Write))
          {
            //Decrypt Data
            byte[] secret = Convert.FromBase64String(EncryptedData);
            cryptoStream.Write(secret, 0, secret.Length);
            cryptoStream.FlushFinalBlock();
            byte[] decryptBytes = memoryStream.ToArray();
            aes.Clear();
            //Update values on UI thread
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
              DecryptedData = Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
            });
          }
        }
      }
    }
    
    public void SaveEncryptedDataToIsolatedStorage()
    {
      //Save secret to Application Settings
      if (EncryptedData != "")
      {
        if (IsolatedStorageSettings.ApplicationSettings.Contains(StorageKeyName))
        {
          IsolatedStorageSettings.ApplicationSettings[StorageKeyName] =
            EncryptedData;
        }
        else
        {
          IsolatedStorageSettings.ApplicationSettings.Add(
            StorageKeyName, EncryptedData);
        }
      }
    }

    public void LoadEncryptedDataFromIsolatedStorage()
    {
      //Retrieve secret from Application Settings
      if (IsolatedStorageSettings.ApplicationSettings.Contains(StorageKeyName))
      {
        Deployment.Current.Dispatcher.BeginInvoke(() =>
        {
          EncryptedData = 
            IsolatedStorageSettings.ApplicationSettings[StorageKeyName].ToString();
        });
      }
      else
      {
        Deployment.Current.Dispatcher.BeginInvoke(() =>
        {
          EncryptedData = "";
        });
      }
    }

    public MainViewModel()
    {
      if (IsInDesignMode)
      {
        // Code runs in Blend --> create design time data.
      }
      else
      {
        // Code runs "for real"
      }
    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}