﻿using System;
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

            volume.setBorderValue(new Density8(0));
            surfaceExtractor = new SurfaceExtractorDensity8(volume, volume.getEntireVolumePaddedBorder(), surface);
        }

        public void Calculate()
        {
            surfaceExtractor.execute();

            Random random = new Random();
            // TODO: redo polyvox wrapper to generate these instead of using LINQ hilarity
            Vertices = surface.getVertices().Select(v =>
                {
                    const double colorDensity = 15.0;
                    Vector3 vector = v.position.ToVector3();
                    // this is super slow, need to do it per cell instead.
                    byte noise = PolyVoxExtensions.PerlinNoise(v.position.ToVector3(), colorDensity);
                    byte rand = (byte)random.Next(255);
                    return new VertexPositionColor(vector,
                        new Color(random.Next(120, 130) + (noise % 50), random.Next(160, 180) + (rand * noise % 20), noise + 30));
                }).ToArray();

            // convert indices to xna format
            Indices = surface.getIndices().Select(i => (short)i).ToArray();
            //Indices = surface.getIndices().Select(i => (short)i).Reverse().ToArray();
        }
    }
}
