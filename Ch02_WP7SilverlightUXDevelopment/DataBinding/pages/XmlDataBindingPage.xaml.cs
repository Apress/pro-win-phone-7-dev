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
using System.Xml;

namespace DataBinding.pages
{
  public partial class XmlDataBindingPage : PhoneApplicationPage
  {
    public XmlDataBindingPage()
    {
      InitializeComponent();
    }

    private void XmlReaderIconButton_Click(object sender, EventArgs e)
    {
      XmlReaderSettings XmlRdrSettings = new XmlReaderSettings();
      XmlRdrSettings.XmlResolver = new XmlXapResolver();
      XmlReader reader = XmlReader.Create("ApressBooks.xml", XmlRdrSettings);

      // Moves the reader to the root element.
      reader.MoveToContent();

      while (!reader.EOF)
      {
        reader.ReadToFollowing("ApressBook");
        // Note that ReadInnerXml only returns the markup of the node's children
        // so the book's attributes are not returned.
        XmlDataReaderListBox.Items.Add(reader.ReadInnerXml());
      }
      reader.Close();
      reader.Dispose();
    }

    private void SwitchListBoxIconButton_Click(object sender, EventArgs e)
    {
      if (XmlDataReaderListBox.Visibility == Visibility.Visible)
      {
        XmlDataReaderListBox.Visibility = Visibility.Collapsed;
        XmlDataLinqListBox.Visibility = Visibility.Visible;
      }
      else
      {
        XmlDataReaderListBox.Visibility = Visibility.Visible;
        XmlDataLinqListBox.Visibility = Visibility.Collapsed;
      }

    }
  }
}