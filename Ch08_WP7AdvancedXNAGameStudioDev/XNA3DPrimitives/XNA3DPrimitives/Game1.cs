using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace XNA3DPrimitives
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    Camera camera;
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    VertexBuffer vertexBuffer;
    IndexBuffer indexBuffer;
    BasicEffect basicEffect;

    //Lists and variables used to construct vertex and index buffer data
    List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
    List<ushort> indices = new List<ushort>();
    float size = 3;

    //Object position info
    float yaw = MathHelper.PiOver4;
    float pitch = MathHelper.PiOver4;
    float roll = MathHelper.PiOver4;
    Vector3 cameraPosition = new Vector3(0, 0, 10f);

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      // Frame rate is 30 fps by default for Windows Phone.
      TargetElapsedTime = TimeSpan.FromTicks(333333);
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    { // Initialize camera
      camera = new Camera(this, cameraPosition,
          Vector3.Zero, Vector3.Up);
      Components.Add(camera);

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
      // Create the Cube
      CreateCubeObject();
      ConstructGraphicsObjectsForDrawingCube();
    }

    #region Code for Construction and Drawing Cube
    private void CreateCubeObject()
    {
      // A cube has six faces, each one pointing in a different direction.
      Vector3[] normals =
            {
                new Vector3(0, 0, 1),
                new Vector3(0, 0, -1),
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, -1, 0),
            };

      // Create each face in turn.
      foreach (Vector3 normal in normals)
      {
        // Get two vectors perpendicular to the cube face normal and
        //perpendicular to each other
        Vector3 triangleSide1 = new Vector3(normal.Y, normal.Z, normal.X);
        Vector3 triangleSide2 = Vector3.Cross(normal, triangleSide1);

        // Six indices (two triangles) per face
        indices.Add((ushort)(vertices.Count + 0));
        indices.Add((ushort)(vertices.Count + 1));
        indices.Add((ushort)(vertices.Count + 2));

        indices.Add((ushort)(vertices.Count + 0));
        indices.Add((ushort)(vertices.Count + 2));
        indices.Add((ushort)(vertices.Count + 3));

        // Four vertices per cube face
        vertices.Add(new VertexPositionNormalTexture(
         (normal - triangleSide1 - triangleSide2) * size / 2, normal,Vector2.Zero));
        vertices.Add(new VertexPositionNormalTexture(
         (normal - triangleSide1 + triangleSide2) * size / 2, normal,Vector2.One));
        vertices.Add(new VertexPositionNormalTexture(
         (normal + triangleSide1 + triangleSide2) * size / 2, normal,Vector2.Zero));
        vertices.Add(new VertexPositionNormalTexture(
         (normal + triangleSide1 - triangleSide2) * size / 2, normal,Vector2.One));
      }
    }

    private void ConstructGraphicsObjectsForDrawingCube()
    {
      // Create a vertex buffer, and copy the cube vertex data into it
      vertexBuffer = new VertexBuffer(graphics.GraphicsDevice,
                                      typeof(VertexPositionNormalTexture),
                                      vertices.Count, BufferUsage.None);
      vertexBuffer.SetData(vertices.ToArray());

      // Create an index buffer, and copy the cube index data into it.
      indexBuffer = new IndexBuffer(graphics.GraphicsDevice, typeof(ushort),
                                    indices.Count, BufferUsage.None);
      indexBuffer.SetData(indices.ToArray());

      // Create a BasicEffect, which will be used to render the primitive.
      basicEffect = new BasicEffect(graphics.GraphicsDevice);
      basicEffect.EnableDefaultLighting();
      basicEffect.PreferPerPixelLighting = true;
    }

    private void DrawCubePrimitive(Matrix world,  Color color)
    {
      // Set BasicEffect parameters.
      basicEffect.World = world;
      basicEffect.View = camera.View;
      basicEffect.Projection = camera.Projection;
      basicEffect.DiffuseColor = color.ToVector3();
      basicEffect.Alpha = color.A / 255.0f;

      GraphicsDevice device = basicEffect.GraphicsDevice;
      device.DepthStencilState = DepthStencilState.Default;

      if (color.A < 255)
      {
        // Set renderstates for alpha blended rendering.
        device.BlendState = BlendState.AlphaBlend;
      }
      else
      {
        // Set renderstates for opaque rendering.
        device.BlendState = BlendState.Opaque;
      }

      GraphicsDevice graphicsDevice = basicEffect.GraphicsDevice;
      // Set our vertex declaration, vertex buffer, and index buffer.
      graphicsDevice.SetVertexBuffer(vertexBuffer);
      graphicsDevice.Indices = indexBuffer;

      foreach (EffectPass effectPass in basicEffect.CurrentTechnique.Passes)
      {
        effectPass.Apply();
        int primitiveCount = indices.Count / 3;
        graphicsDevice.DrawIndexedPrimitives(
          PrimitiveType.TriangleList, 0, 0,vertices.Count, 0, primitiveCount);
      }
    }
    #endregion

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
      vertexBuffer.Dispose();
      indexBuffer.Dispose();
      basicEffect.Dispose(); 
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      float time = (float)gameTime.TotalGameTime.TotalSeconds;

      yaw = time * 0.5f;
      pitch = time * 0.5f;
      roll = time * 0.5f;

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here
      Matrix world = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
      DrawCubePrimitive(world, Color.Orange);    
      // Reset the fill mode renderstate.
      GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
      base.Draw(gameTime);
    }
  }
}
