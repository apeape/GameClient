using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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
using Nuclex.UserInterface.Controls;

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
        float cameraArc = 0;
        float cameraRotation = 0;
        float cameraDistance = 100;
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

        const int cellSize = 32;
        const int terrainSize = 2;

        private float terrainNoiseDensity = 45.0f;

        private bool Initialized = false;
        private bool Wireframe = false;

        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            input = new InputManager(Services, Window.Handle);
            gui = new GuiManager(Services);
            Content.RootDirectory = "Content";

            var capturer = new Nuclex.UserInterface.Input.DefaultInputCapturer(input);
            capturer.ChangePlayerIndex(ExtendedPlayerIndex.Five);
            gui.InputCapturer = capturer;

            // Automatically query the input devices once per update
            Components.Add(input);

            // You can either add the GUI to the Components collection to have it render
            // automatically, or you can call the GuiManager's Draw() method yourself
            // at the appropriate place if you need more control.
            Components.Add(gui);

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            terrainBatch = new PrimitiveBatch<VertexPositionColor>(graphics.GraphicsDevice);
            terrainDrawContext = new BasicEffectDrawContext(graphics.GraphicsDevice);
            terrainDrawContext.BasicEffect.FogEnabled = true;
            terrainDrawContext.BasicEffect.FogColor = new Color(20, 20, 30).ToVector3();
            terrainDrawContext.BasicEffect.FogStart = 50;
            terrainDrawContext.BasicEffect.FogEnd = 100;

            contentManager = new ContentManager(
                GraphicsDeviceServiceHelper.MakePrivateServiceProvider(graphics),
                Content.RootDirectory
            );

            debugDrawer = new DebugDrawer(graphics);

            // Displays a FPS counter
            Components.Add(new FpsComponent(graphics, Content.RootDirectory));

            // Create a new screen. Screens manage the state of a GUI and accept input
            // notifications. If you have an in-game computer display where you want
            // to use a GUI, you can create a second screen for that and thus cleanly
            // separate the state of the in-game computer from your game's own GUI :)
            Viewport viewport = GraphicsDevice.Viewport;
            Screen mainScreen = new Screen(viewport.Width, viewport.Height);
            gui.Screen = mainScreen;

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
            camera = new Camera(
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
            Initialized = true;
        }

        private void GenerateTerrain(int seed)
        {
            if (terrainManager != null)
                if (terrainManager.cellsInitialized > 0
                    && (terrainManager.cellsInitialized < terrainManager.terrainCells.Length))
                    return; // currently generating terrain

            terrainManager = new TerrainManager(terrainSize, cellSize);
            terrainManager.InitializeCells();
            terrainManager.Initialized = false;
            terrainManager.cellsInitialized = 0;
            // generate terrain in a new thread
            new Thread(delegate()
            {
                terrainManager.ForEachCell((pos, cell) =>
                {
                    // do each cell in its own thread
                    new Thread(delegate()
                    {
                        // generate perlin 3d noise
                        Vector3 noiseOffset = pos;
                        noiseOffset.Y *= -1;
                        cell.PerlinNoise(noiseOffset * cellSize, terrainNoiseDensity, seed);
                        // generate mesh for the cell
                        TerrainCellMesh mesh = new TerrainCellMesh(terrainManager.GetCell(pos));
                        mesh.Calculate();
                        if (mesh == null)
                        {
                            throw new Exception("Problem generating mesh from volume!");
                        }
                        terrainManager.terrainCellMeshes[(int)pos.X, (int)pos.Y, (int)pos.Z] = mesh;
                        terrainManager.cellsInitialized++;
                    }).Start();
                });
            }).Start();
            /*
            // test sphere
            terrainManager.terrainCells[0, 0, 0].CreateSphere(18, 255);
            terrainManager.terrainCellMeshes[0, 0, 0] = new TerrainCellMesh(terrainManager.GetCell(0, 0, 0));
            terrainManager.terrainCellMeshes[0, 0, 0].Calculate();
            terrainManager.Initialized = true;*/
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use Content to load your game content here
            GenerateTerrain(14);
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
            if (!terrainManager.Initialized)
                if (terrainManager.cellsInitialized == terrainManager.terrainCells.Length)
                    terrainManager.Initialized = true;

            //camera.HandleControls(gameTime);
            UpdateCamera(gameTime);

            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void DrawTerrain()
        {
            if (terrainManager.Initialized)
            {
                Matrix worldMatrix = Matrix.CreateScale(1.0f, 1.0f, 1.0f)
                    * Matrix.CreateRotationY(MathHelper.Pi)
                    * Matrix.CreateTranslation(new Vector3(0, 0, 0));

                terrainDrawContext.BasicEffect.VertexColorEnabled = true;

                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

                RasterizerState rasterizerState = new RasterizerState();
                if (Wireframe)
                    rasterizerState.FillMode = FillMode.WireFrame;
                else
                    rasterizerState.FillMode = FillMode.Solid;
                rasterizerState.CullMode = CullMode.CullClockwiseFace;
                GraphicsDevice.RasterizerState = rasterizerState;

                // using this we should be able to draw many terrain chunks in one draw call
                terrainBatch.Begin(QueueingStrategy.Deferred);

                terrainManager.ForEachCellMesh((pos, cellMesh) =>
                {
                    //Matrix terrainPos = Matrix.CreateScale(1.025f, 1.025f, 1.025f)
                    Matrix terrainPos = Matrix.CreateScale(0.9f, 0.9f, 0.9f)
                        * Matrix.CreateRotationY(MathHelper.Pi)
                        * Matrix.CreateTranslation(pos * cellSize);

                    terrainDrawContext.BasicEffect.View = camera.View;
                    terrainDrawContext.BasicEffect.Projection = camera.Projection;
                    terrainDrawContext.BasicEffect.World = terrainPos * worldMatrix;

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

                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            }
            else
                debugDrawer.DrawString(new Vector2((Window.ClientBounds.Width / 2) - 50, Window.ClientBounds.Height / 2),
                    "Generating terrain... " + terrainManager.cellsInitialized + "/" + terrainManager.terrainCells.Length,
                    Color.Green);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            // Compute camera matrices
            camera.View = Matrix.CreateTranslation(0, -40, 0) *
                          Matrix.CreateRotationY(MathHelper.ToRadians(cameraRotation)) *
                          Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc)) *
                          Matrix.CreateLookAt(new Vector3(0, 0, -cameraDistance),
                                              new Vector3(0, 0, 0), Vector3.Up);

            terrainDrawContext.BasicEffect.View = camera.View;
            terrainDrawContext.BasicEffect.Projection = camera.Projection;
            terrainDrawContext.BasicEffect.World = Matrix.Identity;

            DrawTerrain();

            debugDrawer.DrawString(new Vector2(20, 50),
@"Controls:
Movement:   W S A D
Zoom:       X Z
Reset:      R
Fog:        F
Wireframe:  Q
Regenerate: T
",
                Color.Orange);
            debugDrawer.Draw(gameTime);
            debugDrawer.Reset();

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
            WindowControl options = new WindowControl();
            options.Title = "Options";
            options.EnableDragging = true;
            options.Bounds = new UniRectangle(
                new UniScalar(1.0f, -210.0f), 10,
                200, 260);
            mainScreen.Desktop.Children.Add(options);

            OptionControl wireFrameToggle = new OptionControl();
            wireFrameToggle.Text = "Wireframe";
            wireFrameToggle.Bounds = new UniRectangle(10, 30, 100, 32);
            wireFrameToggle.Selected = Wireframe;
            wireFrameToggle.Changed += delegate(object sender, EventArgs arguments) { Wireframe = wireFrameToggle.Selected; };
            options.Children.Add(wireFrameToggle);

            OptionControl fogToggle = new OptionControl();
            fogToggle.Text = "Fog";
            fogToggle.Bounds = new UniRectangle(10, 65, 100, 32);
            fogToggle.Selected = terrainDrawContext.BasicEffect.FogEnabled;
            fogToggle.Changed += delegate(object sender, EventArgs arguments) { terrainDrawContext.BasicEffect.FogEnabled = fogToggle.Selected; };
            options.Children.Add(fogToggle);

            LabelControl fogNearLabel = new LabelControl("Near");
            fogNearLabel.Bounds = new UniRectangle(10, 100, 20, 24);
            options.Children.Add(fogNearLabel);

            const float fogRange = 1000f;

            HorizontalSliderControl fogNear = new HorizontalSliderControl();
            fogNear.Bounds = new UniRectangle(50, 100, 140, 24);
            fogNear.ThumbSize = 0.1f;
            fogNear.ThumbPosition = terrainDrawContext.BasicEffect.FogStart / fogRange;
            fogNear.Moved += delegate(object sender, EventArgs arguments) { terrainDrawContext.BasicEffect.FogStart = fogNear.ThumbPosition * fogRange; };
            options.Children.Add(fogNear);

            LabelControl fogFarLabel = new LabelControl("Far");
            fogFarLabel.Bounds = new UniRectangle(10, 125, 20, 24);
            options.Children.Add(fogFarLabel);

            HorizontalSliderControl fogFar = new HorizontalSliderControl();
            fogFar.Bounds = new UniRectangle(50, 125, 140, 24);
            fogFar.ThumbSize = 0.1f;
            fogFar.ThumbPosition = terrainDrawContext.BasicEffect.FogEnd / fogRange;
            fogFar.Moved += delegate(object sender, EventArgs arguments) { terrainDrawContext.BasicEffect.FogEnd = fogFar.ThumbPosition * fogRange; };
            options.Children.Add(fogFar);

            LabelControl densityLabel = new LabelControl("Density");
            densityLabel.Bounds = new UniRectangle(10, 150, 20, 24);
            options.Children.Add(densityLabel);

            const float densityRange = 100.0f;
            HorizontalSliderControl densityControl = new HorizontalSliderControl();
            densityControl.Bounds = new UniRectangle(60, 150, 130, 24);
            densityControl.ThumbSize = 0.1f;
            densityControl.ThumbPosition = terrainNoiseDensity / densityRange;
            densityControl.Moved += delegate(object sender, EventArgs arguments) { terrainNoiseDensity = densityControl.ThumbPosition * densityRange; };
            options.Children.Add(densityControl);

            ButtonControl regenerateButton = new ButtonControl();
            regenerateButton.Text = "Regenerate";
            regenerateButton.Bounds = new UniRectangle(
                new UniScalar(1.0f, -190.0f), new UniScalar(1.0f, -75.0f), 110, 32);
            regenerateButton.Pressed += delegate(object sender, EventArgs arguments) { GenerateTerrain(random.Next(255)); };
            options.Children.Add(regenerateButton);

            ButtonControl resetButton = new ButtonControl();
            resetButton.Text = "Reset Camera";
            resetButton.Bounds = new UniRectangle(
                new UniScalar(1.0f, -190.0f), new UniScalar(1.0f, -40.0f), 110, 32);
            resetButton.Pressed += delegate(object sender, EventArgs arguments) { ResetCamera(); };
            options.Children.Add(resetButton);

            // Button through which the user can quit the application
            ButtonControl quitButton = new ButtonControl();
            quitButton.Text = "Quit";
            quitButton.Bounds = new UniRectangle(
                new UniScalar(1.0f, -70.0f), new UniScalar(1.0f, -40.0f), 60, 32);
            quitButton.Pressed += delegate(object sender, EventArgs arguments) { Exit(); };
            options.Children.Add(quitButton);
        }


        /// <summary>
        /// Handles camera input.
        /// </summary>
        private void UpdateCamera(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check for input to rotate the camera up and down around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                cameraArc += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                cameraArc -= time * 0.1f;
            }

            cameraArc += currentGamePadState.ThumbSticks.Right.Y * time * 0.25f;

            // Limit the arc movement.
            if (cameraArc > 90.0f)
                cameraArc = 90.0f;
            else if (cameraArc < -90.0f)
                cameraArc = -90.0f;

            // Check for input to rotate the camera around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                cameraRotation += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                cameraRotation -= time * 0.1f;
            }

            cameraRotation += currentGamePadState.ThumbSticks.Right.X * time * 0.25f;

            // Check for input to zoom camera in and out.
            if (currentKeyboardState.IsKeyDown(Keys.Z))
                cameraDistance += time * 0.25f;

            if (currentKeyboardState.IsKeyDown(Keys.X))
                cameraDistance -= time * 0.25f;

            cameraDistance += currentGamePadState.Triggers.Left * time * 0.5f;
            cameraDistance -= currentGamePadState.Triggers.Right * time * 0.5f;

            // Limit the camera distance.
            if (cameraDistance > 500.0f)
                cameraDistance = 500.0f;
            else if (cameraDistance < 10.0f)
                cameraDistance = 10.0f;

            if (currentGamePadState.Buttons.RightStick == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(Keys.R))
            {
                ResetCamera();
            }

            if (!previousKeyboardState.IsKeyDown(Keys.Q) && currentKeyboardState.IsKeyDown(Keys.Q))
                Wireframe = !Wireframe;

            if (!previousKeyboardState.IsKeyDown(Keys.F) && currentKeyboardState.IsKeyDown(Keys.F))
                terrainDrawContext.BasicEffect.FogEnabled = !terrainDrawContext.BasicEffect.FogEnabled;

            if (!previousKeyboardState.IsKeyDown(Keys.T) && currentKeyboardState.IsKeyDown(Keys.T))
                GenerateTerrain(random.Next(255));

            previousKeyboardState = currentKeyboardState;
        }

        private void ResetCamera()
        {
            cameraArc = 0;
            cameraRotation = 0;
            cameraDistance = 100;
        }

    }
}
