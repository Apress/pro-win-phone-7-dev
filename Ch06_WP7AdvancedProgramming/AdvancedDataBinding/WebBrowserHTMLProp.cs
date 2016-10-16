using System.Windows;
using Microsoft.Phone.Controls;

namespace AdvancedDataBinding
{
  public static class WebBrowserHTMLProp
  {
    public static readonly DependencyProperty HtmlProperty = 
      DependencyProperty.RegisterAttached(
        "Html", typeof(string), typeof(WebBrowserHTMLProp), 
        new PropertyMetadata(OnHtmlPropChanged));

    public static string GetHtml(DependencyObject dependencyObject)
    {
      return (string)dependencyObject.GetValue(HtmlProperty);
    }
    
    public static void SetHtml(DependencyObject dependencyObject, string value)
    {
      dependencyObject.SetValue(HtmlProperty, value);
    }

    private static void OnHtmlPropChanged(DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      var webBrowser = dependencyObject as WebBrowser;

      if (webBrowser == null)
        return;

      var html = e.NewValue.ToString();
      webBrowser.NavigateToString(html);
    }
  }
}
