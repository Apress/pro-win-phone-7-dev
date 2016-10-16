using System;
using Microsoft.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlienShooter.GameObjects
{
  class GameStatusBoard
  {
    private SpriteFont _gameFont;
    private SoundEffect _alienExplosionSoundEffect;
    private SoundEffect _heroShipDamageSoundEffect;
    private VibrateController _vibrateController = VibrateController.Default;
    private TimeSpan _vibrateTimeSpan = TimeSpan.FromMilliseconds(400);
    private Color _livesTextColor = Color.OldLace;
    //Status Board related fields
    private bool _displayMessage;
    private string _message;

    public GameStatusBoard(SpriteFont gameFont)
    {
      _gameFont = gameFont;
      _score = 0;
      _lives = 3;
    }

    #region Properties
    private int _score;
    public int Score
    {
      get { return _score; }
      set
      {
        _score = value;
        _alienExplosionSoundEffect.Play();
      }
    }

    private int _lives;
    public int Lives
    {
      get { return _lives; }
      set
      {
        _lives = value;
        _heroShipDamageSoundEffect.Play();
        _vibrateController.Start(_vibrateTimeSpan);
      }
    }

    public bool GameOver { get; set; }
    #endregion

    #region Methods
    public void LoadContent(ContentManager content)
    {
      _alienExplosionSoundEffect =
        content.Load<SoundEffect>("SoundEffects/Explosion");
      _heroShipDamageSoundEffect =
        content.Load<SoundEffect>("SoundEffects/HeroShipDamage");
    }

    public void Update(GameTime GameTime)
    {
      switch (Score)
      {
        case 50: _displayMessage = true;
          _message = "Nice Start!";
          break;
        case 60: _displayMessage = false;
          break;
        case 100: _displayMessage = true;
          _message = "Keep It Up!";
          break;
        case 120: _displayMessage = false;
          break;
        default: break;
      }

      switch (_lives)
      {
        case 3: _livesTextColor = Color.LightGreen; 
                break;
        case 2: _livesTextColor = Color.Yellow;
                _displayMessage = true;
                _message = "Waring!";
                break;
        case 1: _livesTextColor = Color.Red;
                _displayMessage = true;
                _message = "Danger!";
                break;
        case 0: _livesTextColor = Color.Red;
                _displayMessage = true;
                _message = "Game Over!";
                GameOver = true;
                break;
      }

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(_gameFont, "SCORE: " + Score, Vector2.Zero,
        Color.OldLace);
    if (_displayMessage)
      spriteBatch.DrawString(_gameFont, _message, new Vector2(175, 0), 
        _livesTextColor);
    if (GameOver)
      spriteBatch.DrawString(_gameFont, _message, new Vector2(175, 370), 
        _livesTextColor);
      spriteBatch.DrawString(_gameFont, "LIVES: " + Lives, new Vector2(395, 0), 
        _livesTextColor);
    }
    #endregion
  }
}