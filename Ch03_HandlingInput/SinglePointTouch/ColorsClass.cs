using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace SinglePointTouch
{
  public class ColorsClass
  {
    List<ColorClass> _colors;
    public ColorsClass()
    {
      _colors = new List<ColorClass>();
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Blue), ColorName = "Blue" });
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Brown), ColorName = "Brown"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Cyan), ColorName = "Cyan"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.DarkGray), 
        ColorName = "DarkGray"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Gray), ColorName = "Gray"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Green), ColorName = "Green"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.LightGray),
        ColorName = "LightGray" });
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Magenta), 
        ColorName = "Magenta" });
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Orange), ColorName="Orange"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Purple), ColorName="Purple"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Red), ColorName = "Red"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.White), ColorName = "White"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Black), ColorName = "Black"});
      _colors.Add(new ColorClass() { 
        ColorBrush = new SolidColorBrush(Colors.Yellow), ColorName = "Yellow"});
    }

    public List<ColorClass> ColorsCollection
    {
      get { return _colors; }
    }
  }

  public class ColorClass
  {
    public Brush ColorBrush { get; set; }
    public String ColorName { get; set; }
  }
}
