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
using SpriteSheetRuntime;

namespace SpriteAnimation
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteSheet SpriteAnimationSpriteSheet;
    int spriteIndex = 0;
    Rectangle screenRect;
    TimeSpan frameElapsedTime = new TimeSpan();
    TimeSpan frameTime = TimeSpan.FromMilliseconds(50d);

    Texture2D backgroundTexture;

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
      backgroundTexture = Content.Load<Texture2D>("Textures/background");
      SpriteAnimationSpriteSheet = Content.Load<SpriteSheet>("Sprites/SpriteAnimationSpriteSheet");
      
      //Get a pointer to the entire screen Rectangle 
      screenRect = graphics.GraphicsDevice.Viewport.Bounds;

      // TODO: use this.Content to load your game content here
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
      // Add elapsed game time between calls to Update
      // Once enough time has passed, i.e. timeToNextFrame > frameTime
      //increment sprite index.  
      frameElapsedTime += gameTime.ElapsedGameTime;
      if (frameElapsedTime > frameTime)
      {
        if (spriteIndex < 9)
          spriteIndex++;
        else spriteIndex = 0;
        frameElapsedTime = TimeSpan.FromMilliseconds(0d);
      }
      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
      spriteBatch.Draw(SpriteAnimationSpriteSheet.Texture,
        new Vector2((screenRect.Width / 2) -
                      SpriteAnimationSpriteSheet.SourceRectangle("heroship"+spriteIndex.ToString()).Width / 2,
                    (screenRect.Height / 2) -
                      SpriteAnimationSpriteSheet.SourceRectangle("heroship" + spriteIndex.ToString()).Height / 2),
        SpriteAnimationSpriteSheet.SourceRectangle("heroship" + spriteIndex.ToString()),
        Color.White);

      spriteBatch.Draw(SpriteAnimationSpriteSheet.Texture,
        new Vector2((screenRect.Width / 3) -
                      SpriteAnimationSpriteSheet.SourceRectangle("spaceship" + spriteIndex.ToString()).Width / 2,
                    (screenRect.Height / 3) -
                      SpriteAnimationSpriteSheet.SourceRectangle("spaceship" + spriteIndex.ToString()).Height / 2),
        SpriteAnimationSpriteSheet.SourceRectangle("spaceship" + spriteIndex.ToString()),
        Color.White);

      spriteBatch.Draw(SpriteAnimationSpriteSheet.Texture,
        new Vector2((screenRect.Width * .6f) -
                      SpriteAnimationSpriteSheet.SourceRectangle("missile" + spriteIndex.ToString()).Width / 2,
                    (screenRect.Height * .6f) -
                      SpriteAnimationSpriteSheet.SourceRectangle("missile" + spriteIndex.ToString()).Height / 2),
        SpriteAnimationSpriteSheet.SourceRectangle("missile" + spriteIndex.ToString()),
        Color.White);
      //spriteBatch.Draw(SpriteAnimationSpriteSheet.Texture, Vector2.One, Color.White);
        spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
