using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AccelerometerInputXNA
{
  static public class  AccelerometerHelper
  {
    static private Vector3 _current3DAcceleration;
    static public Vector3 Current3DAcceleration 
    { 
      get 
      { 
        return _current3DAcceleration; 
      }
      set
      {
        //Set previous to "old" current 3D Acceleration
        _previous3DAcceleration = _current3DAcceleration;

        //Update current 3D Acceleration
        //Take into account screen orientation
        //when assigning values
        switch (Orientation)
        {
          case DisplayOrientation.LandscapeLeft:
            _current3DAcceleration.X = -value.Y;
            _current3DAcceleration.Y = -value.X;
            _current3DAcceleration.Z = -value.Z;
            break;
          case DisplayOrientation.LandscapeRight:
            _current3DAcceleration.X = value.Y;
            _current3DAcceleration.Y = value.X;
            _current3DAcceleration.Z = value.Z;
            break;
          case DisplayOrientation.Portrait:
            _current3DAcceleration.X = value.X;
            _current3DAcceleration.Y = -value.Y;
            _current3DAcceleration.Z = value.Z;
            break;
        }

        //Update current 2D Acceleration
        _current2DAcceleration.X = _current3DAcceleration.X;
        _current2DAcceleration.Y = _current3DAcceleration.Y;
        //Update previous 2D Acceleration
        _previous2DAcceleration.X = _previous3DAcceleration.X;
        _previous2DAcceleration.Y = _previous3DAcceleration.Y;
        //Update deltas
        _xDelta = _current3DAcceleration.X - _previous3DAcceleration.X;
        _yDelta = _current3DAcceleration.Y - _previous3DAcceleration.Y;
        _zDelta = _current3DAcceleration.Z - _previous3DAcceleration.Z;
      }
    }

    static private Vector2 _current2DAcceleration;
    static public Vector2 Current2DAcceleration
    {
      get
      {
        return _current2DAcceleration;
      }
    }

    static private DateTimeOffset _currentTimeStamp;
    static public DateTimeOffset CurrentTimeStamp
        {
      get
      {
        return _currentTimeStamp;
      }
      set
      {
        _previousTimeStamp = _currentTimeStamp;
        _currentTimeStamp = value;
      }
    }

    static private Vector3 _previous3DAcceleration;
    static public Vector3 Previous3DAcceleration 
    { get { return _previous3DAcceleration; } }

    static private Vector2 _previous2DAcceleration;
    static public Vector2 Previous2DAcceleration 
    { get { return _previous2DAcceleration; } }

    static private DateTimeOffset _previousTimeStamp;
    static public DateTimeOffset PreviousTimeStamp 
    { get { return _previousTimeStamp; } }

    static private double _xDelta ;
    static public double XDelta { get { return _xDelta;} }

    static private double _yDelta;
    static public double YDelta { get { return _yDelta; } }

    static private double _zDelta;
    static public double ZDelta { get { return _zDelta; } }

    public static DisplayOrientation Orientation { get; set; }
  }
}
