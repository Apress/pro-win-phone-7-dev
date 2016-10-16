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
using System.Xml.Linq;
using System.IO;

namespace AsynchronousProgramming.pages
{
  public partial class DispatcherPage : PhoneApplicationPage
  {
    public DispatcherPage()
    {
      InitializeComponent();
    }

    private void LoadDataAppBarButton_Click(object sender, EventArgs e)
    {
      Uri location =
         new Uri("http://localhost:9090/xml/ApressBooks.xml", UriKind.Absolute);
      WebRequest request = HttpWebRequest.Create(location);
      request.BeginGetResponse(
          new AsyncCallback(this.RetrieveXmlCompleted), request);
    }

    void RetrieveXmlCompleted(IAsyncResult ar)
    {
      List<ApressBook> _apressBookList;
      HttpWebRequest request = ar.AsyncState as HttpWebRequest;
      WebResponse response = request.EndGetResponse(ar);
      Stream responseStream = response.GetResponseStream();
      using (StreamReader streamreader = new StreamReader(responseStream))
      {
        XDocument xDoc = XDocument.Load(streamreader);
        _apressBookList =
        (from b in xDoc.Descendants("ApressBook")
         select new ApressBook()
         {
           Author = b.Element("Author").Value,
           Title = b.Element("Title").Value,
           ISBN = b.Element("ISBN").Value,
           Description = b.Element("Description").Value,
           PublishedDate = Convert.ToDateTime(b.Element("DatePublished").Value),
           NumberOfPages = b.Element("NumPages").Value,
           Price = b.Element("Price").Value,
           ID = b.Element("ID").Value
         }).ToList();
      }
      //Could use Anonymous delegate (does same as below line of code)
      //BooksListBox.Dispatcher.BeginInvoke(
      //  delegate()
      //  {
      //    DataBindListBox(_apressBookList);
      //  }
      // );
      //Use C# 3.0 Lambda
      
      //BooksListBox.Dispatcher.BeginInvoke(() => DataBindListBox(_apressBookList));
      BooksListBox.Dispatcher.BeginInvoke(() =>
      {
        BooksListBox.ItemsSource = _apressBookList;
      });
    }

    //void DataBindListBox(List<ApressBook> list)
    //{
    //  BooksListBox.ItemsSource = list;
    //}
  }
}