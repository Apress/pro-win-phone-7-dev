using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteSheetRuntime;
using Microsoft.Xna.Framework;

namespace AlienShooter.GameObjects
{
  class AlienGameObject : GameObject
  {
    private static Random randomNumber = new Random(DateTime.Now.Date.Millisecond);
    private int alienSpeed = 5;
    private int alienVelocityArc = 2;

    public AlienGameObject(SpriteSheet loadedTexture,
             string spriteName, Rectangle screenRect)
      : base(loadedTexture, spriteName, screenRect)
    {
      Alive = true;
      ResetGameObject();
    }

    public override void ResetGameObject()
    {
      //Randomize animation
      _spriteIndex = (int)(randomNumber.NextDouble() * NumberOfAnimationFrames);
      //Randomize initial position
      Position = new Vector2(randomNumber.Next(_screenRect.Width), 35);

      //Apply default alien speed
      Velocity = new Vector2(randomNumber.Next(alienVelocityArc), alienSpeed);
      Alive = true;
    }

    public override Rectangle BoundingRect
    {
      get
      {
        Rectangle rect = new Rectangle((int)(Position.X-this.SpriteCenter.X),
                                       (int)(Position.Y-this.SpriteCenter.Y),
          SpriteAnimationSpriteSheet.SourceRectangle(SpriteName + 0).Width,
          SpriteAnimationSpriteSheet.SourceRectangle(SpriteName + 0).Height);
        rect.Inflate(0, 20);
        return rect ;
      }
    }
  }
}
