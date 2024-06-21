using System.Collections.Generic;
using UnityEngine;

namespace TileMapGenerator 
{
    public class TerrainGenerator : MonoBehaviour
    {
        public Tile[] TilePrefabs;
        public int Width;
        public int Height;

        private Dictionary<TileType, Tile> mTileDictionary;

        private TileWave[,] mWaveMap;

        private void Start()
        {
            SetupTilePrefabDictionary();
            GenerateMap(Width, Height);
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        GenerateMap();
        //    }
        //}

        public void GenerateMap(int width, int height)
        {
            Debug.Log("Generating map...");

            Width = width; 
            Height = height;

            //Random.InitState(1923);
            SetupTileWaveMap();
            ClearPreviousMap();
            WaveFunctionCollapse();
            GenerateFinalMap();

            Debug.Log("Done.");
        }

        private void SetupTilePrefabDictionary() 
        {
            mTileDictionary = new Dictionary<TileType, Tile>();

            for (int i = 0; i < TilePrefabs.Length; i++)
            {
                Tile tile = TilePrefabs[i];
                mTileDictionary.Add(tile.TileType, tile);
            }
        }

        private void SetupTileWaveMap() 
        {
            mWaveMap = new TileWave[Height, Width];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    mWaveMap[y, x] = new TileWave();
                }
            }
        }

        private void ClearPreviousMap() 
        {
            foreach(Transform child in transform) 
            {
                GameObject.Destroy(child.gameObject);
            }

            SetupTileWaveMap();
        }

        private void WaveFunctionCollapse() 
        {
            CollapseUpdate();
        }

        private bool CollapseUpdate() 
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TileWave tile = mWaveMap[y, x];

                    if (!tile.IsDetermined)
                    {
                        tile.DetermineTile();
                        CollapseAroundTile(x, y, tile.DeterminedTile);
                    }
                }
            }

            return true;
        }

        private void CollapseAroundTile(int x, int y, TileType tileType) 
        {
            // Collapse Left
            CollapseNeighbour(x-1, y,   tileType);
            
            // Collapse Left-Up
            CollapseNeighbour(x-1, y-1, tileType);
            
            // Collapse Up
            CollapseNeighbour(x  , y-1, tileType);
            
            // Collapse Up-Right
            CollapseNeighbour(x+1, y-1, tileType);
            
            // Collapse Right
            CollapseNeighbour(x+1, y,   tileType);
            
            // Collapse Down-Right
            CollapseNeighbour(x+1, y+1, tileType);
            
            // Collapse Down
            CollapseNeighbour(x  , y+1, tileType);

            // Collapse Down Left
            CollapseNeighbour(x-1, y+1, tileType);
        }

        private void CollapseNeighbour(int x, int y, TileType tileType) 
        {
            // Range Check
            if(x < 0 || x >= Width || y < 0 || y >= Height) 
            {
                return; 
            }

            mWaveMap[y, x].CollapseTile(tileType);
        }

        private void GenerateFinalMap() 
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    GenerateTile(mWaveMap[y, x].DeterminedTile, x, y);
                }
            }
        }

        private void GenerateTile(TileType tileType, int x, int y) 
        {
            if(mTileDictionary.TryGetValue(tileType, out Tile tile)) 
            {
                float height = GetHeight(tileType);
                float xOffset = Width;
                float yOffset = -25.0f;
                GameObject.Instantiate(tile, new Vector3(x * 2.0f - xOffset, height, y * 2.0f - yOffset), tile.transform.rotation, transform);
            }
        }

        private float GetHeight(TileType tileType) 
        {
            float height = 0.0f;    
            switch (tileType) 
            {
                case TileType.SnowForest:
                case TileType.Snow:
                    height = 1.5f;
                    break;
                case TileType.Forest:
                    height = 0.5f;
                    break;
                case TileType.Water:
                    height = -1.0f;
                    break;
                case TileType.Sand:
                    height = -0.5f;
                    break;
                case TileType.Dirt:
                    height = -0.25f;
                    break;
                case TileType.Rock:
                    height = 0.5f;
                    break;
            }

            return height;
        }
    }
}
