using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices.Sensors;

namespace AccelerometerInputXNA
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    Accelerometer accelerometer;
    SpriteFont spriteFontSegoeUIMono;
    Vector2 spriteFontDrawLocation;
    Texture2D StickManTexture;
    GameObject StickManGameObject;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      // Frame rate is 30 fps by default for Windows Phone.
      TargetElapsedTime = TimeSpan.FromTicks(333333);

      this.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);
      
    }

    void Window_OrientationChanged(object sender, EventArgs e)
    {
      AccelerometerHelper.Orientation = this.Window.CurrentOrientation;
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      accelerometer = new Accelerometer();
      accelerometer.Start();
      accelerometer.ReadingChanged += 
        new EventHandler<AccelerometerReadingEventArgs>(accelerometer_ReadingChanged);
      
      base.Initialize();
    }

    //Temp variable to avoid newing up a new Vector object on every reading
    //when assigning to AccelerometerHelper.Current3DAcceleration to help
    //minimize garbage collection
    private Vector3 AccelerometerTemp = new Vector3();
    void accelerometer_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
    {
      //
      AccelerometerTemp.X = (float)e.X;
      AccelerometerTemp.Y = (float)e.Y;
      AccelerometerTemp.Z = (float)e.Z;

      AccelerometerHelper.Current3DAcceleration  = AccelerometerTemp;
      AccelerometerHelper.CurrentTimeStamp = e.Timestamp;
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
      spriteFontSegoeUIMono = Content.Load<SpriteFont>("Pescadero");
      spriteFontDrawLocation = new Vector2(40, 40);
      //Load StickMan texture
      StickManTexture = Content.Load<Texture2D>("StickMan");
      //Create StickMan sprite object
      StickManGameObject = new GameObject(StickManTexture);
      //Position in the middle of the screen
      StickManGameObject.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
      graphics.GraphicsDevice.Viewport.Height / 2);
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here
      StickManGameObject.Velocity += 30*AccelerometerHelper.Current2DAcceleration;
      System.Diagnostics.Debug.WriteLine(AccelerometerHelper.Current3DAcceleration.ToString());
      StickManGameObject.Update(gameTime, GraphicsDevice.Viewport.Bounds);

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here
      spriteBatch.Begin();
      // Draw the string
      spriteBatch.DrawString(spriteFontSegoeUIMono, "Acceleration: "+AccelerometerHelper.Current3DAcceleration.ToString(), spriteFontDrawLocation,
        Color.LightGreen, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
      //Draw the stickman
      StickManGameObject.Draw(spriteBatch);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
