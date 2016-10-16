using Microsoft.Xna.Framework;

namespace XNA3DPrimitives
{
  public class Camera : Microsoft.Xna.Framework.GameComponent
  {
    public Camera(Game game, Vector3 position, Vector3 direction, Vector3 upVector)
      : base(game)
    {
      // Initialize frustum matrix
      Projection = Matrix.CreatePerspectiveFieldOfView(
          MathHelper.PiOver4,
          this.Game.GraphicsDevice.Viewport.AspectRatio,
          1, 20);

      // Initialize "look at" matrix
      View = Matrix.CreateLookAt(position, direction, upVector);
    }

    public Matrix View { get; private set; }
    public Matrix Projection { get; private set; }

    public override void Initialize()
    {

      base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {

      base.Update(gameTime);
    }
  }
}
