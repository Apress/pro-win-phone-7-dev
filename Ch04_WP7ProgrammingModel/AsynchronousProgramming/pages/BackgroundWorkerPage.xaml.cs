using System.ComponentModel;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AsynchronousProgramming.pages
{
  public partial class BackgroundWorkerPage : PhoneApplicationPage
  {
    private BackgroundWorker _worker = new BackgroundWorker();
    //robcamer@microsoft.com
    public BackgroundWorkerPage()
    {
      InitializeComponent();
      //Configure BackgroundWorker thread
      _worker.WorkerReportsProgress = true;
      _worker.WorkerSupportsCancellation = true;
      _worker.DoWork  += new DoWorkEventHandler(worker_DoWork);
      _worker.ProgressChanged += 
        new ProgressChangedEventHandler(worker_ProgressChanged);
      _worker.RunWorkerCompleted += 
        new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

      //Kick off long running process
      //Make status visible
      _worker.RunWorkerAsync();
      StatusStackPanel.Visibility = Visibility.Visible;     
    }

    protected override void OnNavigatedFrom(
                     System.Windows.Navigation.NavigationEventArgs e)
    {
      //Cancel work if user navigates away
      if (_worker.IsBusy)
        _worker.CancelAsync();

      base.OnNavigatedFrom(e);
    }

    void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      ApressBooks books = new ApressBooks();
      books.LoadBooks();
      int progress;
      string state = "initializing...";
      //Do fake work to retrieve and process books
      for (int i = 1; i <= books.ApressBookList.Count;i++ )
      {
        if (_worker.CancellationPending == true)
        {
          e.Cancel = true;
          break;
        }
        else
        {
          progress = (int)System.Math.Round((double)i /
                     books.ApressBookList.Count * 100d);

          if ((progress > 15) && (progress < 90))
            state = "processing..." ;
          if (progress > 85)
            state = "finishing..." ;
          if (progress == 95)
            state = "Loading complete.";

          _worker.ReportProgress(progress, state);
          System.Threading.Thread.Sleep(250);
        }
      }
      e.Result = books;
    }

    void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      BookListDownloadProgress.Value = e.ProgressPercentage;
      processingStateTextBlock.Text = e.UserState as string;
    }


    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled == true)
        MessageBox.Show("Operation cancelled.","Cancelled",MessageBoxButton.OK);
      else
        LayoutRoot.DataContext = e.Result as ApressBooks;

      //Clean up status UI
      BookListDownloadProgress.Value = 0;
      processingStateTextBlock.Text = "";
      StatusStackPanel.Visibility = Visibility.Collapsed;
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      _worker.CancelAsync();
    }
  }
}