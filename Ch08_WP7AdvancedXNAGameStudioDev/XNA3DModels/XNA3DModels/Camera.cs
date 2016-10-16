using System;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace XNA3DModels
{
  public class Camera : Microsoft.Xna.Framework.GameComponent
  {
    #region private members
    //Camera properties
    Vector3 direction;
    Vector3 Up;

    //Accelerometer input
    Accelerometer accelerometer;
    AccelerometerReading currentReading;

    //Movement parameters
    float speed = 100f;
    //Yaw movement
    float maxLeftRight = MathHelper.PiOver4 / 10;
    float stepLeftRight = MathHelper.PiOver4 / 10000;
    float currentLeftRight = 0;
    #endregion

    public Camera(Game game, Vector3 position, Vector3 target, Vector3 upVector)
      : base(game)
    {
      Position = position;
      direction = target - position;
      direction.Normalize();
      Up = upVector;

      // Initialize frustum matrix, which doesn't change
      Projection = Matrix.CreatePerspectiveFieldOfView(
          MathHelper.PiOver4,
          this.Game.GraphicsDevice.Viewport.AspectRatio,
          1, 20000);

      currentReading = new AccelerometerReading();
    }

    public Matrix View
    {
      get { return Matrix.CreateLookAt(Position, Position + direction, Up); }
    }
    public Matrix Projection { get; private set; }
    public Vector3 Position { get; private set; }

    public void ResetPosition()
    {
      Position = new Vector3(0, 1000, 15000);
      direction = Vector3.Zero - Position;
      direction.Normalize();
      Up = Vector3.Up;
    }

    public override void Initialize()
    {
      accelerometer = new Accelerometer();
      accelerometer.ReadingChanged += accelerometer_ReadingChanged;
      accelerometer.Start();

      base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
      ApplyThrust();

      ApplySteering();

      base.Update(gameTime);
    }

    private void ApplySteering()
    {
      if ((Math.Abs(currentLeftRight) < maxLeftRight) &&
    (Math.Abs(currentReading.Y) > .4))
      {
        direction = Vector3.Transform(direction,
                    Matrix.CreateFromAxisAngle(Up, currentLeftRight));
        if (currentReading.Y > .2)
        {
          currentLeftRight += stepLeftRight;
          if (currentLeftRight < 0)
            currentLeftRight = currentLeftRight * -1;
        }
        if (currentReading.Y < -.2)
        {
          currentLeftRight -= stepLeftRight;
          if (currentLeftRight > 0)
            currentLeftRight = currentLeftRight * -1;
        }
      }
    }

    private void ApplyThrust()
    {
      //Travel forward or backwards based on tilting
      //device forwards or backwards (Z axis for Accelerometer)
        if (currentReading.Z < -.65)
          Position += direction * speed;

        if (currentReading.Z > -.5)
          Position -= direction * speed;
    }

    protected override void OnEnabledChanged(object sender, System.EventArgs args)
    {
      if (this.Enabled)
        accelerometer.Start();
      else
        accelerometer.Stop();

      base.OnEnabledChanged(sender, args);
    }

    private void accelerometer_ReadingChanged(
      object sender, AccelerometerReadingEventArgs e)
    {
      currentReading.X = e.X;
      currentReading.Y = e.Y;
      currentReading.Z = e.Z;
      currentReading.Timestamp = e.Timestamp;

#if DEBUG
      System.Diagnostics.Debug.WriteLine("X: " + e.X);
      System.Diagnostics.Debug.WriteLine("Y: " + e.Y);
      System.Diagnostics.Debug.WriteLine("Z: " + e.Z);
#endif

    }
  }

  class AccelerometerReading
  {
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public DateTimeOffset Timestamp { get; set; }
  }
}