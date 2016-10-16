using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA3DModels.Models
{
  public class SceneManager : DrawableGameComponent
  {
    List<GameObject3D> gameObjects3D = new List<GameObject3D>();

    public SceneManager(Game game)
      : base(game)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    protected override void LoadContent()
    {
      gameObjects3D.Add(new GameObject3D(Game.Content.Load<Model>("Models/spaceship")));
      base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
      for (int i = 0; i < gameObjects3D.Count; ++i)
      {
        gameObjects3D[i].Update();
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      for (int i = 0; i < gameObjects3D.Count; i++)
      {
        gameObjects3D[i].Draw(((Game1)Game).Camera);
      }
      base.Draw(gameTime);
    }
  }
}
