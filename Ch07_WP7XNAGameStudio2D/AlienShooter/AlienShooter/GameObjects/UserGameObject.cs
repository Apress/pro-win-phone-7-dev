using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpriteSheetRuntime;
using AlienShooter.GameManagement;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace AlienShooter.GameObjects
{
  class UserGameObject : GameObject
  {
    private Vector2 _leftRightVector = new Vector2(5, 0);
    private AccelerometerState _accelerometerState;
    private SoundEffect _missileLaunchSoundEffect;
    public UserGameObject(SpriteSheet loadedTexture,
             string spriteName, Rectangle screenRect, int maxMissiles)
      : base(loadedTexture, spriteName, screenRect)
    {
      Alive = true;
      MaxNumberofMissiles = 3;
      Missiles = new List<MissileGameObject>();
      for (int i=0;i < MaxNumberofMissiles; i++)
        Missiles.Add(new MissileGameObject(loadedTexture,"missile",screenRect));
    }

    public List<MissileGameObject> Missiles;
    public int MaxNumberofMissiles;

    public void LoadContent(ContentManager content)
    {
      _missileLaunchSoundEffect = 
           content.Load<SoundEffect>("SoundEffects/MissileLaunch");
    }

    public void HandleInput(InputState input)
    {
      //Must check for TouchLocationState as wel as Count
      //Otherwise, FireMissile will be called twice
      //Once for 'Pressed' and once for 'Released'
      if ((input.TouchState.Count > 0) &&
        input.TouchState[0].State == TouchLocationState.Pressed)
      {
        FireMissile();
      }

      _accelerometerState = input.CurrentAccelerometerState;
      if (_accelerometerState.X > .1)
      {
        Velocity = _leftRightVector;
      }
      if (_accelerometerState.X < -.1)
      {
        Velocity = -_leftRightVector;
      }
      //near Zero tilt left or right so
      //set velocity to zero
      if ((_accelerometerState.X < .1) &&
        (_accelerometerState.X > -.1))
        Velocity = Vector2.Zero;
    }

    private void FireMissile()
    {
      for (int i = 0; i < MaxNumberofMissiles; i++)
      {
        if (!Missiles[i].Alive)
        {
          Missiles[i].Alive = true;
          Missiles[i].Position = this.Position;
          _missileLaunchSoundEffect.Play();
          break;
        }
      }
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      if (Position.X < SpriteCenter.X)
        Position = new Vector2(SpriteCenter.X,Position.Y);
      if (Position.X > (_screenRect.Width - SpriteCenter.X))
        Position = new Vector2(_screenRect.Width-SpriteCenter.X,Position.Y);


      for (int i = 0; i < MaxNumberofMissiles; i++)
      {
        if (Missiles[i].Alive == true)
          Missiles[i].Update(gameTime);
      }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
      for (int i = 0; i < MaxNumberofMissiles; i++)
      {
        if (Missiles[i].Alive == true)
          Missiles[i].Draw(gameTime, spriteBatch);
      }
      base.Draw(gameTime, spriteBatch);
    }
  }
}
