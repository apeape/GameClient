﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolyVoxCore;
using Microsoft.Xna.Framework;
using GameClient.Util;
using GameClient.Terrain;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient
{
    public static class PolyVoxExtensions
    {
        /// <summary>
        /// Apply an action to each voxel in the volume
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="action"></param>
        public static void ForEach(this VolumeDensity8 volume, Action<Vector3> action)
        {
            Vector3 pos;
            // loop over entire volume
            for (ushort z = 0; z < volume.getDepth(); z++)
                for (ushort y = 0; y < volume.getHeight(); y++)
                    for (ushort x = 0; x < volume.getWidth(); x++)
                    {
                        pos = new Vector3(x, y, z);
                        action(pos);
                    }
        }

        public static void setDensityAt(this VolumeDensity8 volume, int X, int Y, int Z, byte density)
        {
            //Get the old voxel
            Density8 voxel = volume.getVoxelAt(X, Y, Z);

            //Modify the density
            voxel.setDensity(density);

            //Write the voxel value into the volume
            volume.setVoxelAt(X, Y, Z, voxel);
        }

        public static void setDensityAt(this VolumeDensity8 volume, float X, float Y, float Z, byte density)
        {
            volume.setDensityAt((int)X, (int)Y, (int)Z, density);
        }

        public static void setDensityAt(this VolumeDensity8 volume, Vector3 point, byte density)
        {
            volume.setDensityAt(point.X, point.Y, point.Z, density);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, Vector3 point)
        {
            return volume.getVoxelAt(point.X, point.Y, point.Z);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, int X, int Y, int Z)
        {
            return volume.getVoxelAt((ushort)X, (ushort)Y, (ushort)Z);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, float X, float Y, float Z)
        {
            return volume.getVoxelAt((ushort)X, (ushort)Y, (ushort)Z);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, int X, int Y, int Z, Density8 density)
        {
            return volume.setVoxelAt((ushort)X, (ushort)Y, (ushort)Z, density);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, float X, float Y, float Z, Density8 density)
        {
            return volume.setVoxelAt((ushort)X, (ushort)Y, (ushort)Z, density);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, Vector3 point, Density8 density)
        {
            return volume.setVoxelAt((ushort)point.X, (ushort)point.Y, (ushort)point.Z, density);
        }

        public static void CreateSphere(this VolumeDensity8 volume, float radius, byte density)
        {
            Vector3 volumeCenter = new Vector3(volume.getWidth() / 2, volume.getHeight() / 2, volume.getDepth() / 2);
            volume.CreateSphere(volumeCenter, radius, density);
        }

        public static void CreateSphere(this VolumeDensity8 volume, Vector3 center, float radius, byte density)
        {
            volume.ForEach(
                currentPos =>
                {
                    //Compute how far the current position is from the center of the volume
                    float distToCenter = (currentPos - center).Length();

                    //If the current voxel is less than 'radius' units from the center then we make it solid.
                    if (distToCenter <= radius)
                        volume.setDensityAt(currentPos, density);
                });
        }

        public static void DeleteRandomCells(this VolumeDensity8 volume, byte percentToDelete)
        {
            Random random = new Random();
            volume.ForEach(
                currentPos =>
                {
                    if (random.Next(100) <= percentToDelete)
                        volume.setDensityAt(currentPos, 0);
                });
        }

        public static void PerlinNoise(this VolumeDensity8 volume, double densityDivisor)
        {
            volume.PerlinNoise(densityDivisor, 99);
        }

        public static void PerlinNoise(this VolumeDensity8 volume, Vector3 offset, double densityDivisor)
        {
            volume.PerlinNoise(offset, densityDivisor, 99);
        }

        public static void PerlinNoise(this VolumeDensity8 volume, double densityDivisor, int seed)
        {
            volume.ForEach(curPos => volume.setDensityAt(curPos, PerlinNoise(curPos, densityDivisor, seed)));
        }

        public static void PerlinNoise(this VolumeDensity8 volume, Vector3 offset, double densityDivisor, int seed)
        {
            volume.ForEach(curPos => volume.setDensityAt(curPos, PerlinNoise(curPos - offset, densityDivisor, seed)));
        }

        public static byte PerlinNoise(Vector3 pos, double densityDivisor)
        {
            return PerlinNoise(pos, densityDivisor, 99);
        }

        public static byte PerlinNoise(Vector3 pos, double densityDivisor, int seed)
        {
            PerlinNoise perlinNoise = new PerlinNoise(seed);
            double widthDivisor = 1 / (double)densityDivisor;
            double heightDivisor = 1 / (double)densityDivisor;
            double depthDivisor = 1 / (double)densityDivisor;
            double v =
                // First octave
                (perlinNoise.Noise(2 * pos.X * widthDivisor, 2 * pos.Y * heightDivisor, -2 * pos.Z * depthDivisor) + 1) / 2 * 0.7 +
                // Second octave
                (perlinNoise.Noise(4 * pos.X * widthDivisor, 4 * pos.Y * heightDivisor, 2 * pos.Z * depthDivisor) + 1) / 2 * 0.2;
                // Third octave
                //(perlinNoise.Noise(8 * pos.X * widthDivisor, 8 * pos.Y * heightDivisor, 2 * pos.Z * depthDivisor) + 1) / 2 * 0.1;

            // clamp to 0 - 1
            v = Math.Min(1, Math.Max(0, v));
            byte density = (byte)(v * 255);
            return density;
        }

        public static VolumeDensity8 VolumeDensity8FromVector3(Vector3 dimensions)
        {
            return new VolumeDensity8((ushort)dimensions.X, (ushort)dimensions.Y, (ushort)dimensions.Z, (ushort)Math.Max(Math.Max(dimensions.X, dimensions.Y), dimensions.Z));
        }

        public static VolumeDensity8 VolumeDensity8Cubic(int cubicSize)
        {
            return new VolumeDensity8((ushort)cubicSize, (ushort)cubicSize, (ushort)cubicSize, (ushort)cubicSize);
        }

        public static Region getEntireVolume(this VolumeDensity8 volume)
        {
            return new Region(new Vector3DInt16(0, 0, 0),
                new Vector3DInt16((short)volume.getWidth(), (short)volume.getHeight(), (short)volume.getDepth()));
        }

        public static Region getEntireVolumePaddedBorder(this VolumeDensity8 volume)
        {
            const short padding = 1;
            return new Region(new Vector3DInt16(-padding, -padding, -padding),
                new Vector3DInt16((short)(volume.getWidth()), (short)(volume.getHeight()), (short)(volume.getDepth())));
        }

        public static TerrainCellMesh GetMesh(this VolumeDensity8 volume, Vector3 pos, bool cubic, GraphicsDevice graphicsDevice)
        {
            TerrainCellMesh mesh = new TerrainCellMesh(volume, pos);
            mesh.Calculate(cubic, graphicsDevice);
            return mesh;
        }

        public static Vector3 ToVector3(this Vector3DFloat v)
        {
            return new Vector3(v.getX(), v.getY(), v.getZ());
        }
    }
}
