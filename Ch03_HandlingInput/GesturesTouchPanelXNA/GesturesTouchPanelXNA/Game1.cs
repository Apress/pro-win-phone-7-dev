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
using System.Diagnostics;

namespace GesturesTouchPanelXNA
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    GestureSample gestureSample;
    string gestureInfo;
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
      TouchPanel.EnabledGestures = GestureType.DoubleTap | GestureType.Flick |
        GestureType.FreeDrag | GestureType.Hold | GestureType.HorizontalDrag |
        GestureType.None | GestureType.Pinch | GestureType.PinchComplete |
        GestureType.Tap | GestureType.VerticalDrag | GestureType.DragComplete;

      base.Initialize();
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
      //Code to center text on screen
      //spriteFontDrawLocation = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
      //  graphics.GraphicsDevice.Viewport.Height / 2);
      //Code to locate text in upper left corner
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
      ProcessTouchInput();
      StickManGameObject.Update(gameTime, GraphicsDevice.Viewport.Bounds);

      base.Update(gameTime);
    }

    private void ProcessTouchInput()
    {
      //Obtain raw touch data to perform hit test for selection
      TouchCollection touches = TouchPanel.GetState();
      if ((touches.Count > 0) && (touches[0].State == TouchLocationState.Pressed))
      {
        // map touch to a Point object to hit test
        Point touchPoint = new Point((int)touches[0].Position.X,
                                     (int)touches[0].Position.Y);

        if (StickManGameObject.BoundingBox.Contains(touchPoint))
        {
          StickManGameObject.Selected = true;
          StickManGameObject.Velocity = Vector2.Zero;
        }
      }
      //Process gestures
      while (TouchPanel.IsGestureAvailable)
      {
        gestureSample = TouchPanel.ReadGesture();
        gestureInfo = gestureSample.GestureType.ToString();
        if (StickManGameObject.Selected)
        {
          switch (gestureSample.GestureType)
          {
            case GestureType.Hold:
              StickManGameObject.Rotation += MathHelper.PiOver2;
              break;
            case GestureType.FreeDrag:
              StickManGameObject.Position += gestureSample.Delta;
              break;
            case GestureType.Flick:
              StickManGameObject.Velocity = gestureSample.Delta;
              break;
            case GestureType.Pinch:
              Vector2 FirstFingerCurrentPosition = gestureSample.Position;
              Vector2 SecondFingerCurrentPosition = gestureSample.Position2;
              Vector2 FirstFingerPreviousPosition = FirstFingerCurrentPosition - 
                      gestureSample.Delta;
              Vector2 SecondFingerPreviousPosition = SecondFingerCurrentPosition -
                      gestureSample.Delta2;
              //Calculate distance between fingers for the current and
              //previous finger positions.  Use it as a ration to
              //scale object.  Can have positive and negative scale.
              float CurentPositionFingerDistance = Vector2.Distance(
                FirstFingerCurrentPosition, SecondFingerCurrentPosition);
              float PreviousPositionFingerDistance = Vector2.Distance(
                FirstFingerPreviousPosition, SecondFingerPreviousPosition);

              float zoomDelta = (CurentPositionFingerDistance - 
                                 PreviousPositionFingerDistance) * .03f;
              StickManGameObject.Scale += zoomDelta;
              break;
          }
        }
        //StickManGameObject.Selected = false;
      }

      if (gestureSample.Position2 != Vector2.Zero)
      {
        Debug.WriteLine("gesture Type:      " + gestureSample.GestureType.ToString());
        Debug.WriteLine("gesture Timestamp: " + gestureSample.Timestamp.ToString());
        Debug.WriteLine("gesture Position:  " + gestureSample.Position.ToString());
        Debug.WriteLine("gesture Position2: " + gestureSample.Position2.ToString());
        Debug.WriteLine("gesture Delta:     " + gestureSample.Delta.ToString());
        Debug.WriteLine("gesture Delta2:    " + gestureSample.Delta2.ToString());
      }

      //Reset if user is no longer interacting with StickMan
      //
      if (touches.Count == 0)
      {
        StickManGameObject.Selected = false;
      }
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      // TODO: Add your drawing code here
      spriteBatch.Begin();
      // Draw gesture info
      string output = "Last Gesture: " + gestureInfo;
      // Draw the string
      spriteBatch.DrawString(spriteFontSegoeUIMono, output, spriteFontDrawLocation,
        Color.LightGreen, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
      //Draw the stickman
      StickManGameObject.Draw(spriteBatch);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}












