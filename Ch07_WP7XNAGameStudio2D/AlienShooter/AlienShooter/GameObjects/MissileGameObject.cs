using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteSheetRuntime;
using Microsoft.Xna.Framework;

namespace AlienShooter.GameObjects
{
  class MissileGameObject : GameObject
  {
    public MissileGameObject(SpriteSheet loadedTexture,
             string spriteName, Rectangle screenRect)
      : base(loadedTexture, spriteName, screenRect)
    {
      ResetGameObject();
    }

    public override void ResetGameObject()
    {
      Position = new Vector2(-SpriteCenter.X, _screenRect.Height + SpriteCenter.Y);
      Velocity = new Vector2(0,-5);
      Alive = false;
    }
  }
}
