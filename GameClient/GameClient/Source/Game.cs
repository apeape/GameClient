using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Nuclex.Graphics;
using Nuclex.Graphics.Batching;
using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface.Visuals.Flat;
using Nuclex.UserInterface;
using Nuclex.Input;
using Nuclex.Graphics.Debugging;

using PolyVoxCore;
using GameClient.Util;
using GameClient.Terrain;

namespace GameClient
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>Initializes and manages the graphics device</summary>
        private GraphicsDeviceManager graphics { get; set; }
        /// <summary>Manages the graphical user interface</summary>
        private GuiManager gui { get; set; }
        /// <summary>Manages input devices for the game</summary>
        private InputManager input { get; set; }
        /// <summary>Camera managing the location of the viewer in the scene</summary>
        private Camera camera { get; set; }
        /// <summary>Draws debugging overlays into the scene</summary>
        private DebugDrawer debugDrawer { get; set; }
        /// <summary>Shared content manager containing the game's font</summary>
        private ContentManager contentManager;

        SurfaceMeshPositionMaterialNormal surface = new SurfaceMeshPositionMaterialNormal();
        /// <summary>Primitive batch used to render terrain cells in batches</summary>
        private PrimitiveBatch<VertexPositionColor> terrainBatch;
        private BasicEffectDrawContext terrainDrawContext;

        private Random random = new Random();

        private TerrainManager terrainManager;
        

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.input = new InputManager(Services, Window.Handle);
            this.gui = new GuiManager(Services);
            Content.RootDirectory = "Content";

            var capturer = new Nuclex.UserInterface.Input.DefaultInputCapturer(this.input);
            capturer.ChangePlayerIndex(ExtendedPlayerIndex.Five);
            this.gui.InputCapturer = capturer;

            // Automatically query the input devices once per update
            Components.Add(this.input);

            // You can either add the GUI to the Components collection to have it render
            // automatically, or you can call the GuiManager's Draw() method yourself
            // at the appropriate place if you need more control.
            Components.Add(this.gui);

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            terrainBatch = new PrimitiveBatch<VertexPositionColor>(this.graphics.GraphicsDevice);
            terrainDrawContext = new BasicEffectDrawContext(graphics.GraphicsDevice);

            this.contentManager = new ContentManager(
                GraphicsDeviceServiceHelper.MakePrivateServiceProvider(this.graphics),
                Content.RootDirectory
            );

            this.debugDrawer = new DebugDrawer(this.graphics);

            // Displays a FPS counter
            Components.Add(new FpsComponent(this.graphics, Content.RootDirectory));

            // Create a new screen. Screens manage the state of a GUI and accept input
            // notifications. If you have an in-game computer display where you want
            // to use a GUI, you can create a second screen for that and thus cleanly
            // separate the state of the in-game computer from your game's own GUI :)
            Viewport viewport = GraphicsDevice.Viewport;
            Screen mainScreen = new Screen(viewport.Width, viewport.Height);
            this.gui.Screen = mainScreen;

            // Each screen has a 'desktop' control. This invisible control by default
            // stretches across the whole screen and serves as the root of the control
            // tree in which all visible controls are managed. All controls are positioned
            // using a system of fractional coordinates and pixel offset coordinates.
            // We now adjust the position of the desktop window to prevent GUI or HUD
            // elements from appearing outside of the title-safe area.
            mainScreen.Desktop.Bounds = new UniRectangle(
                new UniScalar(0.0f, 0.0f), new UniScalar(0.0f, 0.0f), // x and y = 10%
                new UniScalar(1.0f, 0.0f), new UniScalar(1.0f, 0.0f) // width and height = 80%
            );

            // Now let's do something funky: add buttons directly to the desktop.
            // This will also show the effect of the title-safe area.
            createDesktopControls(mainScreen);

            // Create a new camera with a perspective projection matrix
            this.camera = new Camera(
                Matrix.CreateLookAt(
                    new Vector3(0.0f, 0.0f, -100.0f), // camera location
                    new Vector3(0.0f, 0.0f, 0.0f), // camera focal point
                    Vector3.Up // up vector for the camera's orientation
                ),
                Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4, // field of view
                    (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, // aspect ratio
                    0.01f, 1000.0f // near and far clipping plane
                )
            );

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            const int terrainSize = 2;
            const int cellSize = 32;
            terrainManager = new TerrainManager(terrainSize, cellSize);
            terrainManager.InitializeCells();
            terrainManager.ForEachCell( (pos, cell) =>
                {
                    // generate perlin 3d noise
                    cell.PerlinNoise(random.Next(100));
                    // generate mesh for the cell
                    TerrainCellMesh mesh = new TerrainCellMesh(terrainManager.GetCell(pos));
                    mesh.Calculate();
                    terrainManager.terrainCellMeshes[(int)pos.X, (int)pos.Y, (int)pos.Z] = mesh;
                });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            camera.HandleControls(gameTime);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.terrainDrawContext.BasicEffect.View = this.camera.View;
            this.terrainDrawContext.BasicEffect.Projection = this.camera.Projection;
            this.terrainDrawContext.BasicEffect.World = Matrix.Identity;
            terrainDrawContext.BasicEffect.VertexColorEnabled = true;
            
            // using this we should be able to draw many terrain chunks in one draw call
            terrainBatch.Begin(QueueingStrategy.Deferred);
            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            terrainManager.ForEachCellMesh( (pos, cellMesh) =>
                {
                    terrainBatch.Draw(
                        cellMesh.Vertices,
                        0,
                        cellMesh.Vertices.Length,
                        cellMesh.Indices, 
                        0,
                        cellMesh.Indices.Length, 
                        PrimitiveType.TriangleList,
                        terrainDrawContext);
                });
            //terrainBatch.Draw(terrainVertices, 0, terrainVertices.Length, terrainIndices, 0, terrainIndices.Length, PrimitiveType.TriangleList, terrainDrawContext);
            terrainBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        ///   Create gui controls
        /// </summary>
        /// <param name="mainScreen">
        ///   Screen to whose desktop the controls will be added
        /// </param>
        private void createDesktopControls(Screen mainScreen)
        {
            // Button through which the user can quit the application
            ButtonControl quitButton = new ButtonControl();
            quitButton.Text = "Quit";
            quitButton.Bounds = new UniRectangle(
              new UniScalar(1.0f, -84.0f), new UniScalar(1.0f, -38.0f), 80, 32
            );
            quitButton.Pressed += delegate(object sender, EventArgs arguments) { Exit(); };
            mainScreen.Desktop.Children.Add(quitButton);
        }
    }
}
