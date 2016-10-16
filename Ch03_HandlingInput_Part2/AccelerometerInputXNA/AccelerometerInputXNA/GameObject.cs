using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AccelerometerInputXNA
{
  class GameObject
  {
    private const float _minScale = .4f;
    private const float _maxScale = 6f;
    private const float _friction = .7f;
    private const float _bounceVelocity = .9f;


    private float _scale = 1f;
    private Vector2 _velocity;
    private Vector2 _position;

    public GameObject(Texture2D gameObjectTexture)
    {
      Rotation = 0f;
      Position = Vector2.Zero;
      SpriteTexture = gameObjectTexture;
      Center = new Vector2(SpriteTexture.Width / 2, SpriteTexture.Height / 2);
      Velocity = Vector2.Zero;
      TintColor = Color.White;
      Selected = false;
    }

    public Texture2D SpriteTexture { get; set; }
    public Vector2 Center { get; set; }
    public float Rotation { get; set; }
    public Rectangle TouchArea { get; set; }
    public Color TintColor { get; set; }
    public bool Selected { get; set; }
    public float Scale
    {
      get { return _scale; }
      set
      {
        _scale = MathHelper.Clamp(value, _minScale, _maxScale);
      }
    }
    public Vector2 Position
    { get { return _position; }
      set { _position = value ; } //Move position to Center.
    }
    public Vector2 Velocity 
    { 
      get {return _velocity;}
      set { _velocity = value; }
    }

    public Rectangle BoundingBox
    {
      get
      {
        Rectangle rect = 
          new Rectangle((int)(Position.X - SpriteTexture.Width / 2 * Scale),
          (int)(Position.Y - SpriteTexture.Height / 2 * Scale),
          (int)(SpriteTexture.Width * Scale),
          (int)(SpriteTexture.Height * Scale));
          //Increase the touch target a bit
          rect.Inflate(10, 10);
        return rect;
      }
    }

    public void Update(GameTime gameTime, Rectangle displayBounds)
    {
      //apply scale for pinch / zoom gesture
      float halfWidth = (SpriteTexture.Width * Scale) / 2f;
      float halfHeight = (SpriteTexture.Height * Scale) / 2f;

      // apply friction to slow down movement for simple physics when flicked
      Velocity *= 1f - (_friction * (float)gameTime.ElapsedGameTime.TotalSeconds);

      // Calculate position
      //position = velociy * time
      //TotalSeconds is the amount of time since last update in seconds
      Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

      // Apply "bounce" if sprite approaches screen bounds
      if (Position.Y < displayBounds.Top + halfHeight)
      {
        _position.Y = displayBounds.Top + halfHeight;
        _velocity.Y *= -_bounceVelocity;
      }
      if (Position.Y > displayBounds.Bottom - halfHeight)
      {
        _position.Y = displayBounds.Bottom - halfHeight;
        _velocity.Y *= -_bounceVelocity;
      } if (Position.X < displayBounds.Left + halfWidth)
      {
        _position.X = displayBounds.Left + halfWidth;
        _velocity.X *= -_bounceVelocity;
      }

      if (Position.X > displayBounds.Right - halfWidth)
      {
        _position.X = displayBounds.Right - halfWidth;
        _velocity.X *= -_bounceVelocity;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(SpriteTexture, Position, null, TintColor, Rotation,
        Center,Scale,
        SpriteEffects.None,0);
    }
  }
}
