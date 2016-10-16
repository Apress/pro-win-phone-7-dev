#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.IO.IsolatedStorage;
#endregion

namespace AlienShooter.Screens
{
  /// <summary>
  /// The main menu screen is the first thing displayed when the game starts up.
  /// </summary>
  class MainMenuScreen : MenuScreen
  {
    #region Initialization

    const string AlienShooterStateFileName = "AlienShooter.dat";

    /// <summary>
    /// Constructor fills in the menu contents.
    /// </summary>
    public MainMenuScreen()
      : base("Main Menu")
    {
      // Create our menu entries.
      MenuEntry newGameMenuEntry = new MenuEntry("New Game");
      MenuEntry optionsMenuEntry = new MenuEntry("Game Options");

      // Hook up menu event handlers.
      newGameMenuEntry.Selected += newGameMenuEntrySelected;
      optionsMenuEntry.Selected += OptionsMenuEntrySelected;

      // Add entries to the menu.
      MenuEntries.Add(newGameMenuEntry);
      MenuEntries.Add(optionsMenuEntry);
      
      //robcamer - Only display resume game menu opion 
      //if saved state is present.
      using (IsolatedStorageFile gameStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (gameStorage.FileExists(AlienShooterStateFileName))
        {
          MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
          resumeGameMenuEntry.Selected += resumeGameMenuEntry_Selected;
          MenuEntries.Add(resumeGameMenuEntry);
        }
      }
    }

    #endregion

    #region Handle Input


    /// <summary>
    /// Event handler for when the Play Game menu entry is selected.
    /// </summary>
    void newGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
      //
      using (IsolatedStorageFile gameStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (gameStorage.FileExists(AlienShooterStateFileName))
        {
          gameStorage.DeleteFile(AlienShooterStateFileName);
        }
      }

      LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                         new GameplayScreen());
    }

    /// <summary>
    /// RobCamer - Event handler for when the Resume Game menu entry is selected.
    /// </summary>
    void resumeGameMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
    {
      LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                         new GameplayScreen());
    }


    /// <summary>
    /// Event handler for when the Options menu entry is selected.
    /// </summary>
    void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
      ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
    }


    /// <summary>
    /// When the user cancels the main menu, we exit the game.
    /// </summary>
    protected override void OnCancel(PlayerIndex playerIndex)
    {
      ScreenManager.Game.Exit();
    }


    #endregion
  }
}
