using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using PhotosExtraApp.ViewModel;

namespace PhotosExtraApp
{
  /// <summary>
  /// Description for ImageEditorView.
  /// </summary>
  public partial class PictureEditorView : PhoneApplicationPage
  {
    PictureEditingViewModel vm;
    /// <summary>
    /// Initializes a new instance of the ImageEditorView class.
    /// </summary>
    public PictureEditorView()
    {
      InitializeComponent();

      vm = DataContext as PictureEditingViewModel;

      //Register for message to undo changes
      Messenger.Default.Register<bool>(
        this, "UndoImageChanges",
        (param) => UndoImageChanges());

      Messenger.Default.Register<bool>(
        this, "UpdateWriteableBitMap",
        (param) => UpdateWriteableBitMap());
    }

    private void UpdateWriteableBitMap()
    {
      //Update WriteableBitMap so it is ready to save
      vm.ModifiedPicture.Render(WriteableBitMapSourceArea, null);
    }

    private void UndoImageChanges()
    {
      //Undo Image changes
      //Reset WriteableBitMapSourceArea Grid to just hold
      //the original image content only
      Image img = ImageToEdit;
      WriteableBitMapSourceArea.Children.Clear();
      WriteableBitMapSourceArea.Children.Add(img);
    }

    private void EditAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.EditImageCommand.Execute(null);
    }

    private void saveAppBarBtn_Click(object sender, System.EventArgs e)
    {
      vm.SaveImageCommand.Execute(null);
    }

    private void SaveAsAppMenItem_Click(object sender, System.EventArgs e)
    {
      vm.SaveImageAsCommand.Execute(vm.ImageSaveAsName);
    }

    private void RevertToLastSaveMenuItem_Click(object sender, System.EventArgs e)
    {
      vm.RevertToSavedImageCommand.Execute(null);
    }
  }
}