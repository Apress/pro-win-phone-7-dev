#region Using Statements
using System;
using AlienShooter.GameManagement;
using AlienShooter.Screens;
//For Tombstoning Support
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
//Particle Systems
using AlienShooter.ParticleSystem;
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

    //Add Explosions and Smoke Game Components to AlienShooter
    ExplosionSmokeParticleSystem explosionSmokeParticleSystem;
    ExplosionParticleSystem explosionParticleSystem;
    SmokePlumeParticleSystem smokePlumeParticleSystem;
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

      //Add Explosions and Smoke Game Components to AlienShooter
      explosionSmokeParticleSystem = new ExplosionSmokeParticleSystem(this, 2);
      Components.Add(explosionSmokeParticleSystem);
      explosionParticleSystem = new ExplosionParticleSystem(this, 1);
      Components.Add(explosionParticleSystem);
      smokePlumeParticleSystem = new SmokePlumeParticleSystem(this, 8);
      Components.Add(smokePlumeParticleSystem);

      // attempt to deserialize the screen manager from disk. if that
      // fails, we add our default screens.
      if (!screenManager.DeserializeState())
      {
        // Activate the first screens.
        screenManager.AddScreen(new BackgroundScreen(), null);
        screenManager.AddScreen(new MainMenuScreen(), null);
      }
      //Imeplement tombstoning support globally if managing
      //multiple state objects across game gamescreens
      PhoneApplicationService.Current.Activated += AlienGame_Activated;
      PhoneApplicationService.Current.Deactivated += AlienGame_Deactivated;
    }

    void AlienGame_Activated(object sender, ActivatedEventArgs e)
    {
      //Globally handle return from tombstoning here
      if (!screenManager.DeserializeState())
      {
        // Activate the first screens.
        // Resume at Main Menu so that user isn't caught off guard
        screenManager.AddScreen(new BackgroundScreen(), null);
        screenManager.AddScreen(new MainMenuScreen(), null);
      }
    }

    void AlienGame_Deactivated(object sender, DeactivatedEventArgs e)
    {
      //Globally handle  tombstoning here
      GameScreen[] screens = screenManager.GetScreens();
      foreach (GameScreen screen in screens)
        if (screen is GameplayScreen)
        {
          (screen as GameplayScreen).SaveAlienShooterState();
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
