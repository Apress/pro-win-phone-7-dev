using System;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;

namespace DataPersistence.pages
{
  public partial class BasicIsoStorage : PhoneApplicationPage
  {
    private const string fileName = "notes.dat";

    public BasicIsoStorage()
    {
      InitializeComponent();
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
      //Load "settings"
      if (IsolatedStorageSettings.ApplicationSettings.Contains("EnablePush"))
        enableNotifications.IsChecked = 
          (bool)IsolatedStorageSettings.ApplicationSettings["EnablePush"];
      if (IsolatedStorageSettings.ApplicationSettings.Contains("FavColor"))
        colorListBox.SelectedIndex = 
          (int)IsolatedStorageSettings.ApplicationSettings["FavColor"];
      if (IsolatedStorageSettings.ApplicationSettings.Contains("NickName"))
        nicknameTextBox.Text = 
          (string)IsolatedStorageSettings.ApplicationSettings["NickName"];
     
      //Load "notes" text to file
      using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (isf.FileExists(fileName))
        {
          using (IsolatedStorageFileStream fs = 
            isf.OpenFile(fileName, System.IO.FileMode.Open))
          {
            using (StreamReader reader = new StreamReader(fs))
            {
              notesTextBox.Text = reader.ReadToEnd();
              reader.Close();
            }
          }
        }
      }

    }

    private void SaveData()
    {
      //Save "settings"
      IsolatedStorageSettings.ApplicationSettings["EnablePush"] = 
        enableNotifications.IsChecked;
      IsolatedStorageSettings.ApplicationSettings["FavColor"] = 
        colorListBox.SelectedIndex;
      IsolatedStorageSettings.ApplicationSettings["NickName"] = 
        nicknameTextBox.Text;

      //Save "notes" text to file
      using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
      {
        using (IsolatedStorageFileStream fs = 
          isf.OpenFile(fileName, System.IO.FileMode.Create))
        {
          using (StreamWriter writer = new StreamWriter(fs))
          {
            writer.Write(notesTextBox.Text);
            writer.Flush();
            writer.Close();
          }
        }
      }
    }
  }
}

//Retrieve file from xap
//URL format =>("filename.txt") or if the filename.txt file is
//in a directory named files it would be("files/filename.txt") 
//StreamResourceInfo streamResourceInfo = 
//  Application.GetResourceStream(new Uri("EULA.txt", UriKind.Relative));
//using (TextReader textReader = new StreamReader(streamResourceInfo.Stream))
//{
//    textBlock1.Text = textReader.ReadToEnd();
//}