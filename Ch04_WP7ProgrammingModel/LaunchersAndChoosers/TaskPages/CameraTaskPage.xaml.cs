using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

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
      if ((null == e.Error) && (null != e.ChosenPhoto))
        capturedImage.SetSource(e.ChosenPhoto);
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
        using (IsolatedStorageFileStream fs =
          iso.OpenFile(fileName, System.IO.FileMode.Create))
        {
          bmp.SaveJpeg(fs, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
          savedImage.Source = bmp;
        }
      }
    }
  }
}