using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Nuclex.Graphics;

using DeviceEventHandler = System.EventHandler<System.EventArgs>;

namespace GameClient
{
    /// <summary>
    ///   Game component that tracks and displays the game's frame rate
    /// </summary>
    public class FpsComponent : GameComponent, IDrawable
    {
        /// <summary>Triggered when the component's drawing order changes</summary>
        public event DeviceEventHandler DrawOrderChanged { add { } remove { } }

        /// <summary>Triggered when the component's visible property changes</summary>
        public event DeviceEventHandler VisibleChanged { add { } remove { } }

        /// <summary>Initializes a new frame rate tracking component</summary>
        /// <param name="graphicsDeviceService">
        ///   Graphics device service the counter uses for rendering
        /// </param>
        public FpsComponent(
          IGraphicsDeviceService graphicsDeviceService, string contentRootDirectory
        ) :
            base(null)
        {
            this.graphicsDeviceService = graphicsDeviceService;
            this.contentRoot = contentRootDirectory;

            this.frameTimes = new Queue<long>();
            Visible = true;
        }

        /// <summary>Immediately releases all resources used by the instance</summary>
        /// <param name="calledByUser">Whether the call was initiated by user code</param>
        protected override void Dispose(bool calledByUser)
        {
            if (calledByUser)
            {
                if (this.spriteBatch != null)
                {
                    this.spriteBatch.Dispose();
                    this.spriteBatch = null;
                }
            }
        }

        /// <summary>
        ///   Allows the game component to perform any initialization it needs to
        ///   before starting to run. This is where it can query for any required
        ///   services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.contentManager = new ContentManager(
              GraphicsDeviceServiceHelper.MakePrivateServiceProvider(
                this.graphicsDeviceService
              ),
              this.contentRoot
            );
            this.lucidaFont = this.contentManager.Load<SpriteFont>("Lucida");
            this.spriteBatch = new SpriteBatch(this.graphicsDeviceService.GraphicsDevice);
        }

        /// <summary>Determines the drawing order of this component</summary>
        public int DrawOrder { get; private set; }

        /// <summary>Whether the component will draw itself</summary>
        public bool Visible { get; private set; }

        /// <summary>Current number of frames per second achieved</summary>
        public float Fps { get { return this.currentFps; } }

        /// <summary>Called when the game component should draw itself</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            updateFrameTimes(gameTime);
            calculateFps(gameTime);

            if (Visible)
            {
                this.spriteBatch.Begin();

                try
                {
                    string fpsText = string.Format("FPS: {0:0.##}", this.currentFps);

                    this.spriteBatch.DrawString(
                      this.lucidaFont, fpsText, new Vector2(10.0f, 10.0f), Color.Red
                    );
                }
                finally
                {
                    this.spriteBatch.End();
                }
            }
        }

        /// <summary>Updates the frame times array</summary>
        /// <param name="gameTime">Current snapshot of the game's timing values</param>
        private void updateFrameTimes(GameTime gameTime)
        {
            long nowTicks = gameTime.TotalGameTime.Ticks;

            // Remove all frame times older than one second, but do not empty
            // the queue.
            long oneSecondAgoTicks = nowTicks - TimeSpan.TicksPerSecond;
            while (frameTimes.Count > 1)
            {
                if (this.frameTimes.Peek() >= oneSecondAgoTicks)
                {
                    break;
                }

                this.frameTimes.Dequeue();
            }

            // Add the new frame time to the queue
            this.frameTimes.Enqueue(nowTicks);
        }

        /// <summary>Recalculates the frames per second</summary>
        /// <param name="gameTime">Snapshot of the game's timing values</param>
        private void calculateFps(GameTime gameTime)
        {
            if (this.frameTimes.Count < 2)
            {
                this.currentFps = 0.0f;
                return;
            }

            long oldestTicks = this.frameTimes.Peek();
            long nowTicks = gameTime.TotalGameTime.Ticks;
            long delta = nowTicks - oldestTicks;

            long frameCount = this.frameTimes.Count - 1;

            this.currentFps = (float)(frameCount * TimeSpan.TicksPerSecond) / (float)delta;
        }

        /// <summary>Shared content manager containing the game's font</summary>
        private ContentManager contentManager;
        /// <summary>Graphics device service through which rendering takes place</summary>
        private IGraphicsDeviceService graphicsDeviceService;

        /// <summary>Font used for the FPS counter</summary>
        private SpriteFont lucidaFont;
        /// <summary>Sprite batch used to draw the FPS counter</summary>
        private SpriteBatch spriteBatch;

        /// <summary>Stores the times at which a frame was drawn</summary>
        private Queue<long> frameTimes;
        /// <summary>Current frames per second being drawn</summary>
        private float currentFps;

        private string contentRoot;

    }
}
