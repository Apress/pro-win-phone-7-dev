using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SpriteSheetRuntime;
using System.ComponentModel;

namespace AlienShooter.GameObjects
{
  class GameObject
  {
    protected Rectangle _screenRect;
    protected int _spriteIndex;

    public GameObject(SpriteSheet loadedTexture, string spriteName, Rectangle screenRect)
    {
      SpriteAnimationSpriteSheet = loadedTexture;
      SpriteCenter = new Vector2(
        SpriteAnimationSpriteSheet.SourceRectangle(spriteName + 0).Width / 2,
        SpriteAnimationSpriteSheet.SourceRectangle(spriteName + 0).Height / 2);
      //Used to access sprite in SpriteSheet
      //Assume starts at 0 so SpriteName+0 is first Sprite frame for animation
      //NumberOfAnimationFrames is how many sprite frames that are available
      SpriteName = spriteName;
      _screenRect = screenRect;

      //Default initialization
      FrameTime = TimeSpan.FromMilliseconds(100d);
      NumberOfAnimationFrames = 10;
      Position = Vector2.Zero;
      ElapsedFrameTime = TimeSpan.FromMilliseconds(0d);
      Velocity = Vector2.Zero;
      Rotation = 0f;
      Alive = false;
    }

    public SpriteSheet SpriteAnimationSpriteSheet { get; set; }
    public string SpriteName { get; private set; }
    public int NumberOfAnimationFrames { get; set; }
    public TimeSpan FrameTime { get; set; }
    public TimeSpan ElapsedFrameTime { get; set; }
    public Vector2 SpriteCenter { get; set; }
    public bool Alive { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Rotation { get; set; }

    public virtual Rectangle BoundingRect
    {
      get
      {
        return new Rectangle((int)Position.X, (int)Position.Y,
          SpriteAnimationSpriteSheet.SourceRectangle(SpriteName + 0).Width,
          SpriteAnimationSpriteSheet.SourceRectangle(SpriteName + 0).Height);
      }
    }

    public virtual void ResetGameObject()
    {
      Position = Vector2.Zero;
      Velocity = Vector2.Zero;
      Alive = false;
    }

    public virtual void Update(GameTime GameTime)
    {
      if (Alive)
      {
        Position += Velocity;
        //Check screen bounds
        if ((Position.X < 0) ||
            (Position.X > _screenRect.Width) ||
            (Position.Y < 0) ||
            (Position.Y > _screenRect.Height))
          ResetGameObject();
        //Update animation
        UpdateAnimation(GameTime);
      }
    }

    private void UpdateAnimation(GameTime gameTime)
    {
      ElapsedFrameTime += gameTime.ElapsedGameTime;
      if (ElapsedFrameTime > FrameTime)
      {
        if (_spriteIndex < NumberOfAnimationFrames - 1)
          _spriteIndex++;
        else _spriteIndex = 0;
        ElapsedFrameTime = TimeSpan.FromMilliseconds(0d);
      }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (Alive)
      {
        spriteBatch.Draw(SpriteAnimationSpriteSheet.Texture, Position - SpriteCenter,
          SpriteAnimationSpriteSheet.SourceRectangle(SpriteName + _spriteIndex.ToString()),
          Color.White);
      }
    }
  }
}
