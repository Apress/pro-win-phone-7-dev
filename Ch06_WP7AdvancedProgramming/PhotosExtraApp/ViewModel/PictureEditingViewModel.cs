using System.IO;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Xna.Framework.Media;

namespace PhotosExtraApp.ViewModel
{
  public class PictureEditingViewModel : ViewModelBase
  {
    private MediaLibrary mediaLibrary;

    public PictureEditingViewModel()
    {
      mediaLibrary = new MediaLibrary();

      //Register to receive message with picture object from Picture Hub
      //This message is sent from MainPage.xaml.cs in OnNavigateTo
      Messenger.Default.Register<Picture>(
       this, "PictureToEdit",
       (picture) => { PictureToEdit = picture; });

      //Instantiate Commands
      SaveImageCommand = new RelayCommand(
        () => SaveImage());

      SaveImageAsCommand = new RelayCommand<string>(
        param => SaveImageAs(param));

      RevertToSavedImageCommand = new RelayCommand(
        () => EditImage());
      EditImageCommand = new RelayCommand(
        () => RevertToSavedImage());
    }

    #region Image State
    public const string PictureToEditPropertyName = "PictureToEdit";
    private Picture _pictureToEdit = null;
    public Picture PictureToEdit
    {
      get
      {
        return _pictureToEdit;
      }

      set
      {
        if (_pictureToEdit == value)
        {
          return;
        }
        var oldValue = _pictureToEdit;
        _pictureToEdit = value;
        RaisePropertyChanged(PictureToEditPropertyName);
      }
    }


    public const string ModifiedPicturePropertyName = "ModifiedPicture";
    private WriteableBitmap _modifiedPicture = null;
    public WriteableBitmap ModifiedPicture
    {
      get { return _modifiedPicture; }
      set
      {
        if (_modifiedPicture == value)
        { return; }
        var oldValue = _modifiedPicture;
        _modifiedPicture = value;
        RaisePropertyChanged(ModifiedPicturePropertyName);
      }
    }

    //Property to data bind to for SaveAs input UI
    //Used for SaveAs command
    public const string ImageSaveAsNamePropertyName = "ImageSaveAsName";
    private string _imageSaveAsName = null;
    public string ImageSaveAsName
    {
      get { return _imageSaveAsName;}
      set
      {
        if (_imageSaveAsName == value)
        { return; }
        var oldValue = _imageSaveAsName;
        _imageSaveAsName = value;
        RaisePropertyChanged(ImageSaveAsNamePropertyName);
      }
    }

    public const string ImageIsDirtyPropertyName = "ImageIsDirty";
    private bool _imageIsDirety = false;
    public bool ImageIsDirty
    {
      get { return _imageIsDirety; }
      set
      {
        if (_imageIsDirety == value)
        { return; }

        var oldValue = _imageIsDirety;
        _imageIsDirety = value;
        RaisePropertyChanged(ImageIsDirtyPropertyName);
      }
    }
    #endregion

    #region Image Actions for RelayCommand Objects
    private void EditImage()
    {
      //Editing, unsaved changes pending
      ImageIsDirty = true;
    }

    //View must set the writable bitmap area
    //prior to executing this command
    //This Save action takes a new name
    private void SaveImageAs(string saveAsName)
    {
      using (MemoryStream jpegStream = new MemoryStream())
      {
        //Tell the UI to update the WriteableBitMap property
        Messenger.Default.Send<bool>(true, "UpdateWriteableBitMap");
        ModifiedPicture.SaveJpeg(jpegStream, ModifiedPicture.PixelWidth,
          ModifiedPicture.PixelHeight, 0, 100);
        //Update current Picture to reflect new modified image
        PictureToEdit = mediaLibrary.SavePicture(saveAsName, jpegStream);
        //Saved, not editing
        ImageIsDirty = false;
      };
    }

    //View must set the writable bitmap area
    //prior to executing this command
    //This save action overwrites existing image
    private void SaveImage()
    {
      using (MemoryStream jpegStream = new MemoryStream())
      {
        //Tell the UI to update the WriteableBitMap property
        Messenger.Default.Send<bool>(true, "UpdateWriteableBitMap");
        ModifiedPicture.SaveJpeg(jpegStream, ModifiedPicture.PixelWidth,
          ModifiedPicture.PixelHeight, 0, 100);
        //Update current Picture to reflect new modified image
        PictureToEdit = mediaLibrary.SavePicture(PictureToEdit.Name, jpegStream);
        //Saved, not editing
        ImageIsDirty = false;
      };
    }

    //PictureEditingView registers to receive this message
    //It would clear out any edits at the UI level.
    private void RevertToSavedImage()
    {
      Messenger.Default.Send<bool>(true, "UndoImageChanges");
    }
    #endregion

    #region Image Editing Commmands
    public RelayCommand SaveImageCommand { get; private set; }
    public RelayCommand<string> SaveImageAsCommand { get; private set; }
    public RelayCommand EditImageCommand { get; private set; }
    public RelayCommand RevertToSavedImageCommand { get; private set; }
    #endregion
  }
}