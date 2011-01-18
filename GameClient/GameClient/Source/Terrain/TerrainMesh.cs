using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolyVoxCore;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameClient.Terrain
{
    public class TerrainCellMesh
    {
        private SurfaceExtractorDensity8 surfaceExtractor;
        private CubicSurfaceExtractorWithNormalsDensity8 cubicSurfaceExtractor;
        private SurfaceMeshPositionMaterialNormal surface;

        public VertexPositionColor[] Vertices { get; set; }
        public VertexPositionNormalTexture[] VerticesNormal { get; set; }
        public short[] Indices { get; set; }
        public Vector3 Position { get; set; }

        public VertexBuffer vertexBuffer { get; set; }
        public IndexBuffer indexBuffer { get; set; }

        public TerrainCellMesh(VolumeDensity8 volume, Vector3 pos)
        {
            surface = new SurfaceMeshPositionMaterialNormal();
            Position = pos;
            volume.setBorderValue(new Density8(0));
            surfaceExtractor = new SurfaceExtractorDensity8(volume, volume.getEntireVolumePaddedBorder(), surface);
            cubicSurfaceExtractor = new CubicSurfaceExtractorWithNormalsDensity8(volume, volume.getEntireVolumePaddedBorder(), surface);
        }

        public void Calculate(bool cubic, GraphicsDevice graphicsDevice)
        {
            if (cubic)
                cubicSurfaceExtractor.execute();
            else
                surfaceExtractor.execute();

            Random random = new Random();
            // TODO: redo polyvox wrapper to generate these instead of using LINQ hilarity

            /*Vertices = surface.getVertices().Select(v =>
                {
                    const double colorDensity = 15.0;
                    Vector3 vector = v.position.ToVector3();
                    // this is super slow, need to do it per cell instead.
                    byte noise = PolyVoxExtensions.PerlinNoise(v.position.ToVector3(), colorDensity);
                    byte rand = (byte)random.Next(255);
                    return new VertexPositionColor(vector,
                        new Color(random.Next(120, 130) + (noise % 50), random.Next(160, 180) + (rand * noise % 20), noise + 30));
                }).ToArray();*/

            PositionMaterialNormalVector vertices = surface.getVertices();
            if (vertices.Count == 0)
            {
                //Console.WriteLine("skipping empty cell mesh");
            }
            else
            {
                VerticesNormal = vertices.Select<PositionMaterialNormal, VertexPositionNormalTexture>(v =>
                {
                    return new VertexPositionNormalTexture(v.position.ToVector3(), v.getNormal().ToVector3(), new Vector2(0, 0));
                }).ToArray();

                // convert indices to xna format
                Indices = surface.getIndices().Select(i => (short)i).ToArray();

                vertexBuffer = new VertexBuffer(graphicsDevice,
                      typeof(VertexPositionNormalTexture), VerticesNormal.Length,
                      BufferUsage.None);
                vertexBuffer.SetData<VertexPositionNormalTexture>(VerticesNormal);

                indexBuffer = new IndexBuffer(graphicsDevice,
                    IndexElementSize.SixteenBits, Indices.Length, BufferUsage.None);
                indexBuffer.SetData<short>(Indices);
            }

        }
    }
}
