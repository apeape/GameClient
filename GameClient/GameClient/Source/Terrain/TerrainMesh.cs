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
        private SurfaceMeshPositionMaterialNormal surface;

        public VertexPositionColor[] Vertices { get; set; }
        public short[] Indices { get; set; }

        public TerrainCellMesh(VolumeDensity8 volume)
        {
            surface = new SurfaceMeshPositionMaterialNormal();
            surfaceExtractor = new SurfaceExtractorDensity8(volume, volume.getEntireVolume(), surface);
        }

        public void Calculate()
        {
            surfaceExtractor.execute();

            Random random = new Random();
            // TODO: redo polyvox wrapper to generate these instead of using LINQ hilarity
            Vertices = surface.getVertices().Select(v =>
                {
                    Vector3 vector = v.position.ToVector3();
                    // this is super slow, need to do it per cell instead.
                    byte noise = PolyVoxExtensions.PerlinNoise(v.position.ToVector3());
                    return new VertexPositionColor(vector,
                        new Color(noise, noise, noise));
                }).ToArray();

            // convert indices to xna format
            //Indices = surface.getIndices().Select(i => (short)i).ToArray();
            Indices = surface.getIndices().Select(i => (short)i).Reverse().ToArray();
        }
    }
}
