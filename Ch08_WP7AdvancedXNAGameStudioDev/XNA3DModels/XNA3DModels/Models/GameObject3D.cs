using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNA3DModels.Models
{
  class GameObject3D
  {
    Matrix[] boneTransforms;

    public GameObject3D(Model m)
    {
      SpaceshipModel = m;
      boneTransforms = new Matrix[SpaceshipModel.Bones.Count];
    }

    public Matrix World
    {
      get { return Matrix.Identity; }
    }
    public Model SpaceshipModel { get; protected set; }

    public void Update()
    {

    }

    public void Draw(Camera camera)
    {
      SpaceshipModel.CopyAbsoluteBoneTransformsTo(boneTransforms);

      foreach (ModelMesh mesh in SpaceshipModel.Meshes)
      {
        foreach (BasicEffect be in mesh.Effects)
        {
          be.World = World * mesh.ParentBone.Transform;
          be.Projection = camera.Projection;
          be.View = camera.View;
          be.TextureEnabled = true;
          be.EnableDefaultLighting();
          be.PreferPerPixelLighting = true;
        }
        mesh.Draw();
      }
    }
  }
}