using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PolyVoxCore;

namespace GameClient.Terrain
{
    public class TerrainManager
    {
        public VolumeDensity8[, ,] terrainCells { get; set; }
        public Vector3 terrainDimensions { get; set; }
        public Vector3 cellDimensions { get; set; }
        public TerrainCellMesh[, ,] terrainCellMeshes { get; set; }
        public bool Initialized { get; set; }
        public int cellsInitialized { get; set; }

        public TerrainManager(int cubicSize, int cubicCellSize)
            : this(cubicSize, cubicSize, cubicSize, cubicCellSize, cubicCellSize, cubicCellSize)
        {
        }

        public TerrainManager(Vector3 dimensions, Vector3 cellDimensions)
            : this(dimensions.X, dimensions.Y, dimensions.Z, cellDimensions.X, cellDimensions.Y, cellDimensions.Z)
        {
        }

        public TerrainManager(float width, float height, float depth, float cellWidth, float cellHeight, float cellDepth)
            : this((int)width, (int)height, (int)depth, (int)cellWidth, (int)cellHeight, (int)cellDepth)
        {
        }

        public TerrainManager(int width, int height, int depth, int cellWidth, int cellHeight, int cellDepth)
        {
            Initialized = false;
            cellsInitialized = 0;
            terrainDimensions = new Vector3(width, height, depth);
            this.cellDimensions = new Vector3(cellWidth, cellHeight, cellDepth);
            terrainCells = new VolumeDensity8[width, height, depth];
            terrainCellMeshes = new TerrainCellMesh[width, height, depth];
        }

        public void ForEachCell(Action<Vector3> action)
        {
            Vector3 pos;
            // loop over every terrain cell
            for (int z = 0; z < terrainDimensions.Z; z++)
                for (int y = 0; y < terrainDimensions.Y; y++)
                    for (int x = 0; x < terrainDimensions.X; x++)
                    {
                        pos = new Vector3(x, y, z);
                        action(pos);
                    }
        }

        public void ForEachCell(Action<Vector3, VolumeDensity8> action)
        {
            Vector3 pos;
            // loop over every terrain cell
            for (int z = 0; z < terrainDimensions.Z; z++)
                for (int y = 0; y < terrainDimensions.Y; y++)
                    for (int x = 0; x < terrainDimensions.X; x++)
                    {
                        pos = new Vector3(x, y, z);
                        action(pos, terrainCells[x, y, z]);
                    }
        }

        public void ForEachCellMesh(Action<Vector3, TerrainCellMesh> action)
        {
            Vector3 pos;
            // loop over every terrain cell
            for (int z = 0; z < terrainDimensions.Z; z++)
                for (int y = 0; y < terrainDimensions.Y; y++)
                    for (int x = 0; x < terrainDimensions.X; x++)
                    {
                        pos = new Vector3(x, y, z);
                        action(pos, terrainCellMeshes[x, y, z]);
                    }
        }

        public void InitializeCells()
        {
            ForEachCell(pos => InitializeCell(pos.X, pos.Y, pos.Z));
        }

        public void InitializeCell(Vector3 cell)
        {
            InitializeCell(cell.X, cell.Y, cell.Z);
        }

        public void InitializeCell(float X, float Y, float Z)
        {
            InitializeCell((int)X, (int)Y, (int)Z);
        }

        public void InitializeCell(int X, int Y, int Z)
        {
            terrainCells[X, Y, Z] = PolyVoxExtensions.VolumeDensity8FromVector3(cellDimensions);
        }

        public void SetCell(Vector3 cell, VolumeDensity8 volume)
        {
            SetCell(cell.X, cell.Y, cell.Z, volume);
        }

        public void SetCell(float X, float Y, float Z, VolumeDensity8 volume)
        {
            SetCell((int)X, (int)Y, (int)Z, volume);
        }

        public void SetCell(int X, int Y, int Z, VolumeDensity8 volume)
        {
            terrainCells[X, Y, Z] = volume;
        }

        public VolumeDensity8 GetCell(Vector3 cell)
        {
            return GetCell(cell.X, cell.Y, cell.Z);
        }

        public VolumeDensity8 GetCell(float X, float Y, float Z)
        {
            return GetCell((int)X, (int)Y, (int)Z);
        }

        public VolumeDensity8 GetCell(int X, int Y, int Z)
        {
            return terrainCells[X, Y, Z];
        }

        public TerrainCellMesh GetCellMesh(Vector3 cell)
        {
            return GetCellMesh(cell.X, cell.Y, cell.Z);
        }

        public TerrainCellMesh GetCellMesh(float X, float Y, float Z)
        {
            return GetCellMesh((int)X, (int)Y, (int)Z);
        }

        public TerrainCellMesh GetCellMesh(int X, int Y, int Z)
        {
            return terrainCellMeshes[X, Y, Z];
        }
    }
}
