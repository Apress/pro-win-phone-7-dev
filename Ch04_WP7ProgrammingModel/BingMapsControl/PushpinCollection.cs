using System.Collections.Generic;
using System.Device.Location;

namespace BingMapsControl
{
  public class PushpinCollection
  {
    private List<PlotPoint> points;
    public List<PlotPoint> Points { get { return points; } }

    public void InitializePointsCollection()
    {
      //Generate sample data to plot
      points = new List<PlotPoint>()
      {
        new PlotPoint()
        { Quantity = 50,
          Location= new GeoCoordinate(35d, -86d)
        },
        new PlotPoint()
        { Quantity = 40,
          Location= new GeoCoordinate(33d, -85d)
        },
         new PlotPoint()
        { Quantity = 60,
          Location= new GeoCoordinate(34d, -83d)
        },
        new PlotPoint()
        { Quantity = 20,
          Location= new GeoCoordinate(40d, -120d)
        },
      };
    }
  }

  public class PlotPoint
  {
    public int Quantity { get; set; }
    public GeoCoordinate Location { get; set; }
  }
}
