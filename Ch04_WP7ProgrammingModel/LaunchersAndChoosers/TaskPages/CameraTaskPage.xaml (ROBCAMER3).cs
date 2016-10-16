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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;

namespace LaunchersAndChoosers.TaskPages
{
  public partial class CameraTaskPage : PhoneApplicationPage
  {
    private CameraCaptureTask cameraTask;
    private BitmapImage capturedImage;
    string fileName = "capturedimage.jpg";

    public CameraTaskPage()
    {
      InitializeComponent();

      cameraTask = new CameraCaptureTask();
      capturedImage = new BitmapImage();

      cameraTask.Completed += new EventHandler<PhotoResult>(cameraTask_Completed);
      PreviewImage.Source = capturedImage;
    }

    void cameraTask_Completed(object sender, PhotoResult e)
    {
      if (null == e.Error)
      {
        if (null != e.ChosenPhoto)
          capturedImage.SetSource(e.ChosenPhoto);
      }
      else
      {
        MessageBox.Show(e.Error.Message);
      }
    }

    private void TakePictureButton_Click(object sender, EventArgs e)
    {
      cameraTask.Show();
    }

    private void SavePictureButton_Click(object sender, EventArgs e)
    {
      WriteableBitmap bmp = new WriteableBitmap(capturedImage);


      using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
      {
        using (

           IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.CreateNew, iso))
        {
          bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
        }
      }
    }
  }
}