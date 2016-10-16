#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using AlienShooter.GameManagement;
using AlienShooter.Screens;
#endregion

namespace AlienShooter
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class AlienShooterGame : Microsoft.Xna.Framework.Game
  {
    #region private fields
    GraphicsDeviceManager graphics;
    ScreenManager screenManager;
    #endregion

    #region Game Initialization
    public AlienShooterGame()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      TargetElapsedTime = TimeSpan.FromTicks(333333);

      // you can choose whether you want a landscape or portrait
      // game by using one of the two helper functions defined below
      InitializePortraitGraphics();
      // InitializeLandscapeGraphics();

      // Create the screen manager component.
      screenManager = new ScreenManager(this);

      Components.Add(screenManager);

      // attempt to deserialize the screen manager from disk. if that
      // fails, we add our default screens.
      if (!screenManager.DeserializeState())
      {
        // Activate the first screens.
        screenManager.AddScreen(new BackgroundScreen(), null);
        screenManager.AddScreen(new MainMenuScreen(), null);
      }
    }

    protected override void OnExiting(object sender, System.EventArgs args)
    {
      // serialize the screen manager whenever the game exits
      screenManager.SerializeState();

      base.OnExiting(sender, args);
    }

    /// <summary>
    /// Helper method to the initialize the game to be a portrait game.
    /// </summary>
    private void InitializePortraitGraphics()
    {
      graphics.PreferredBackBufferWidth = 480;
      graphics.PreferredBackBufferHeight = 800;
    }

    /// <summary>
    /// Helper method to initialize the game to be a landscape game.
    /// </summary>
    private void InitializeLandscapeGraphics()
    {
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 480;
    }
    #endregion

    #region Draw
    protected override void Draw(GameTime gameTime)
    {
      //Override just to set background to Black
      graphics.GraphicsDevice.Clear(Color.Black);
      // The real drawing happens 
      //inside the screen manager component.
      base.Draw(gameTime);
    }
    #endregion
  }
}
