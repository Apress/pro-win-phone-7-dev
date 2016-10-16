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

namespace HelloXNA
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    //Define Texture2D objects to hold game content
    Texture2D HeroShip;
    Texture2D SpaceShip;
    Texture2D BackgroundImage;
    Texture2D Missile;

    //Define Speed and Position vectors for objects that move
    Vector2 HeroShipPosition;
    Vector2 HeroShipSpeed;
    Vector2 SpaceShipPosition;
    Vector2 SpaceShipSpeed;
    Vector2 MissilePosition;
    Vector2 MissileSpeed;

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

      HeroShip = this.Content.Load<Texture2D>("Sprites/heroship");
      SpaceShip = this.Content.Load<Texture2D>("Sprites/spaceship");
      Missile = this.Content.Load<Texture2D>("Sprites/missile");
      BackgroundImage = this.Content.Load<Texture2D>("Textures/background");

      InitializeObjects();
    }

    private void InitializeObjects()
    {
      //Initialize Positon and Speed
      SpaceShipPosition = new Vector2(
        graphics.GraphicsDevice.Viewport.Width / 2 - SpaceShip.Width / 2, -SpaceShip.Height);
      SpaceShipSpeed = new Vector2(0, 2); // 2 pixels / frame "down"

      //Center hero ship width wise along the X axis
      //Place hero ship with 20 pixels underneath it in the Y axis
      HeroShipPosition = new Vector2(
        graphics.GraphicsDevice.Viewport.Width / 2 - HeroShip.Width / 2,
        graphics.GraphicsDevice.Viewport.Height - HeroShip.Height - 20f);
      HeroShipSpeed = Vector2.Zero;

      //Center Missile on Space Ship and put it 50 pixels further down
      //off screen "below" hereoship
      MissilePosition = HeroShipPosition +
        new Vector2(HeroShip.Width / 2 - Missile.Width / 2, HeroShip.Height + 20f);
      MissileSpeed = new Vector2(0, -6); // 6 pixels / frame "up"

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
      CheckScreenBoundaryCollision(gameTime);
      CheckForCollisions(gameTime);

      HeroShipPosition += HeroShipSpeed;
      SpaceShipPosition += SpaceShipSpeed;
      MissilePosition += MissileSpeed;

      base.Update(gameTime);
    }

    private void CheckForCollisions(GameTime gameTime)
    {
      //Alien and Missile
      Rectangle AlienRec = new Rectangle((int)SpaceShipPosition.X,
                  (int)SpaceShipPosition.Y, SpaceShip.Width, SpaceShip.Height);
      Rectangle MissileRec = new Rectangle((int)MissilePosition.X,
                  (int)MissilePosition.Y, Missile.Width, Missile.Height);

      if (AlienRec.Intersects(MissileRec))
      {
        SpaceShipPosition.Y = -2 * SpaceShip.Height;
        MissilePosition.Y = graphics.GraphicsDevice.Viewport.Height - HeroShip.Height;
      }
    }

    private void CheckScreenBoundaryCollision(GameTime gameTime)
    {
      //Reset Missile if off the screen
      if (MissilePosition.Y < 0)
      {
        MissilePosition.Y = graphics.GraphicsDevice.Viewport.Height -
                            HeroShip.Height;
      }

      //Reset enemy spaceship if off the screen
      //to random drop point
      if (SpaceShipPosition.Y >
              graphics.GraphicsDevice.Viewport.Height)
      {
        SpaceShipPosition.Y = -2 * SpaceShip.Height;
      }
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
      spriteBatch.Draw(BackgroundImage, graphics.GraphicsDevice.Viewport.Bounds, Color.White);
      spriteBatch.Draw(SpaceShip, SpaceShipPosition, Color.White);
      spriteBatch.Draw(Missile, MissilePosition, Color.White);
      spriteBatch.Draw(HeroShip, HeroShipPosition, Color.White);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
