#region File Description
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Threading;
using AlienShooter.GameManagement;
using AlienShooter.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteSheetRuntime;
using System.IO.IsolatedStorage;
using System.IO;
using AlienShooter.ParticleSystem;
#endregion

namespace AlienShooter.Screens
{
  /// <summary>
  /// This screen implements the actual game logic. It is just a
  /// placeholder to get the idea across: you'll probably want to
  /// put some more interesting gameplay in here!
  /// </summary>
  class GameplayScreen : GameScreen
  {
    #region Fields
    ContentManager content;
    SpriteFont gameFont;
    SpriteSheet alienShooterSpriteSheet;
    Texture2D backgroundTexture;
    Vector2 backgroundPosition;
    Rectangle screenRect;

    //Game objects
    GameStatusBoard statusBoard;
    List<AlienGameObject> enemies;
    int maxEnemies = 5;
    UserGameObject heroShip;
    int maxMissiles = 3;
    const string AlienShooterStateFileName = "AlienShooter.dat";
    #endregion


    #region Initialization
    /// <summary>
    /// Constructor.
    /// </summary>
    public GameplayScreen()
    {
      TransitionOnTime = TimeSpan.FromSeconds(1.5);
      TransitionOffTime = TimeSpan.FromSeconds(0.5);
    }


    /// <summary>
    /// Load graphics content for the game.
    /// </summary>
    public override void LoadContent()
    {
      if (content == null)
        content = new ContentManager(ScreenManager.Game.Services, "Content");

      gameFont = content.Load<SpriteFont>("gamefont");

      alienShooterSpriteSheet = content.Load<SpriteSheet>("Sprites/AlienShooterSpriteSheet");

      backgroundTexture = content.Load<Texture2D>("Textures/background");
      backgroundPosition = new Vector2(0, 34);

      //Get a pointer to the entire screen Rectangle 
      screenRect = ScreenManager.GraphicsDevice.Viewport.Bounds;

      LoadAlienShooterState();

      // A real game would probably have more content than this sample, so
      // it would take longer to load. We simulate that by delaying for a
      // while, giving you a chance to admire the beautiful loading screen.
      Thread.Sleep(1000);

      // once the load has finished, we use ResetElapsedTime to tell the game's
      // timing mechanism that we have just finished a very long frame, and that
      // it should not try to catch up.
      ScreenManager.Game.ResetElapsedTime();
    }


    /// <summary>
    /// Unload graphics content used by the game.
    /// </summary>
    public override void UnloadContent()
    {
      content.Unload();
    }


    #endregion

    #region Update and Draw


    /// <summary>
    /// Updates the state of the game. This method checks the GameScreen.IsActive
    /// property, so the game will stop updating when the pause menu is active,
    /// or if you tab away to a different application.
    /// </summary>
    public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                    bool coveredByOtherScreen)
    {
      if (IsActive)
      {
        if (!statusBoard.GameOver)
        {
          CheckForCollisions();

          heroShip.Update(gameTime);
          statusBoard.Update(gameTime);

          for (int i = 0; i < maxEnemies; i++)
          {
            enemies[i].Update(gameTime);
          }
        }
      }
      base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
    }

private void CheckForCollisions()
{
  //Checking for two major collisions
  //1 - Has an in flight missile intersected an alien spaceship - score 5 pts
  for (int i = 0; i < heroShip.MaxNumberofMissiles; i++)
    if (heroShip.Missiles[i].Alive)
      for (int j = 0; j < maxEnemies; j++)
        if ((enemies[j].Alive) &&
            (enemies[j].BoundingRect.Intersects(heroShip.Missiles[i].BoundingRect)))
        {
          statusBoard.Score += 5;  
          //Display Explosion
          ((ExplosionSmokeParticleSystem)ScreenManager.Game.Components[1]).AddParticles(enemies[j].Position);
          ((ExplosionParticleSystem)ScreenManager.Game.Components[2]).AddParticles(enemies[j].Position); 
          enemies[j].ResetGameObject();
          heroShip.Missiles[i].ResetGameObject();
          }
  //2 - Has an alien spaceship intersected the hero ship - deduct a life
  for (int j = 0; j < maxEnemies; j++)
    if ((enemies[j].Alive) && (enemies[j].Position.Y > 600) &&
        (enemies[j].BoundingRect.Intersects(heroShip.BoundingRect)))
    {
      statusBoard.Lives -= 1;
      ((SmokePlumeParticleSystem)ScreenManager.Game.Components[3]).AddParticles(heroShip.Position);
              
      for (int i = 0; i < maxEnemies; i++)
        enemies[i].ResetGameObject();
      for (int i = 0; i < heroShip.MaxNumberofMissiles; i++)
        heroShip.Missiles[i].ResetGameObject();
    }
}

    /// <summary>
    /// Lets the game respond to player input. Unlike the Update method,
    /// this will only be called when the gameplay screen is active.
    /// </summary>
    public override void HandleInput(InputState input)
    {
      if (input == null)
        throw new ArgumentNullException("input not initialized");

      // Look up inputs for the active player profile.
      int playerIndex = (int)ControllingPlayer.Value;

      KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
      GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

      // if the user pressed the back button, we save state if Game in progress
      //and return to the main menu
      PlayerIndex player;
      if (input.IsNewButtonPress(Buttons.Back, ControllingPlayer, out player))
      {
        //RobCamer
        //Check game status
        //save state
        SaveAlienShooterState();

        LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
      }
      else
      {
        // Otherwise handle input for user controlled objects
        heroShip.HandleInput(input);
      }
    }

    /// <summary>
    /// Draws the gameplay screen.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
      ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                         Color.SlateBlue, 0, 0);

      // Our player and enemy are both actually just text strings.
      SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

      spriteBatch.Begin();
      //Draw Background
      spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
      //Draw Status Board
      statusBoard.Draw(gameTime, spriteBatch);
      //Draw Hero Ship
      heroShip.Draw(gameTime, spriteBatch);
      //Draw enemies
      if (!statusBoard.GameOver)
      {
        for (int i = 0; i < maxEnemies; i++)
        {
          enemies[i].Draw(gameTime, spriteBatch);
        }
      }
      spriteBatch.End();

      // If the game is transitioning on or off, fade it out to black.
      if (TransitionPosition > 0)
        ScreenManager.FadeBackBufferToBlack(1f - TransitionAlpha);
    }


    #endregion

    #region State Persistence

    public void SaveAlienShooterState()
    {
      //Only save game if not GameOver
      using (IsolatedStorageFile gameStorage = 
        IsolatedStorageFile.GetUserStoreForApplication())
      {
        //Overwrite existing saved game state
        if (gameStorage.FileExists(AlienShooterStateFileName))
        {
          gameStorage.DeleteFile(AlienShooterStateFileName);
        }
        if (!statusBoard.GameOver)
        {
          using (IsolatedStorageFileStream fs =
            gameStorage.OpenFile(AlienShooterStateFileName, 
                                 System.IO.FileMode.Create))
          {
            using (StreamWriter streamWriter = new StreamWriter(fs))
            {
              //Only serialize interesting state
              //Other state MUST be initialized each time
              streamWriter.WriteLine(heroShip.Position.X);
              streamWriter.WriteLine(heroShip.Position.Y);
              streamWriter.WriteLine(heroShip.Velocity.X);
              streamWriter.WriteLine(heroShip.Velocity.Y);
              streamWriter.WriteLine(statusBoard.Score);
              streamWriter.WriteLine(statusBoard.Lives);
              for (int i = 0; i < maxEnemies; i++)
              {
                streamWriter.WriteLine(enemies[i].Alive);
                streamWriter.WriteLine(enemies[i].Position.X);
                streamWriter.WriteLine(enemies[i].Position.Y);
                streamWriter.WriteLine(enemies[i].Velocity.X);
                streamWriter.WriteLine(enemies[i].Velocity.Y);
              }
              streamWriter.Flush();
              streamWriter.Close();
            }
          }
        }
      }
    }

    public void LoadAlienShooterState()
    {
      using (IsolatedStorageFile gameStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        //Initialize all objects as before
        enemies = new List<AlienGameObject>();
        for (int i = 0; i < maxEnemies; i++)
        {
          enemies.Add(new AlienGameObject(alienShooterSpriteSheet, "spaceship", screenRect));
        }

        //Initialize Player Object
        heroShip = new UserGameObject(alienShooterSpriteSheet, "heroship", screenRect, maxMissiles);
        heroShip.Position = new Vector2(screenRect.Width / 2, 720);
        heroShip.LoadContent(content);

        //Initialize Status Board
        statusBoard = new GameStatusBoard(gameFont);
        statusBoard.LoadContent(content);

        //Set saved state on objects
        if (gameStorage.FileExists(AlienShooterStateFileName))
        {
          using (IsolatedStorageFileStream fs =
          gameStorage.OpenFile(AlienShooterStateFileName, System.IO.FileMode.Open))
          {
            using (StreamReader streamReader = new StreamReader(fs))
            {
              heroShip.Position = new Vector2(
                (float)Convert.ToDouble(streamReader.ReadLine()),
                (float)Convert.ToDouble(streamReader.ReadLine()));
              heroShip.Velocity = new Vector2(
                (float)Convert.ToDouble(streamReader.ReadLine()),
                (float)Convert.ToDouble(streamReader.ReadLine()));
              statusBoard.Score = Convert.ToInt32(streamReader.ReadLine());
              statusBoard.Lives = Convert.ToInt32(streamReader.ReadLine());
              for (int i = 0; i < maxEnemies; i++)
              {
                enemies[i].Alive = Convert.ToBoolean(streamReader.ReadLine());
                enemies[i].Position = new Vector2(
                  (float)Convert.ToDouble(streamReader.ReadLine()),
                  (float)Convert.ToDouble(streamReader.ReadLine()));
                enemies[i].Velocity = new Vector2(
                  (float)Convert.ToDouble(streamReader.ReadLine()),
                  (float)Convert.ToDouble(streamReader.ReadLine()));
              }
              streamReader.Close();
            }
          }
        }
      }
    }
    #endregion
  }
}