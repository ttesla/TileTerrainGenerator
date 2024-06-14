using System.Collections.Generic;

namespace TileMapGenerator 
{
    public class TileWave
    {
        public bool IsDetermined       { get; private set; }
        public TileType DeterminedTile { get; private set; }

        private readonly List<TileType> mPossibleTiles;

        public TileWave()
        {
            mPossibleTiles = new List<TileType>
            {
                TileType.Grass,
                TileType.Sand,
                TileType.Dirt,
                TileType.Rock,
                TileType.Snow,
                TileType.Forest,
                TileType.SnowForest,
                TileType.Water
            };
        }

        public void DetermineTile() 
        {
            if (mPossibleTiles.Count > 0)
            {
                DeterminedTile = mPossibleTiles[UnityEngine.Random.Range(0, mPossibleTiles.Count)];
                IsDetermined = true;
            }
            else
            {
                DeterminedTile = TileType.Grass;
                IsDetermined = true;

                UnityEngine.Debug.LogWarning("Determination forced to! " + DeterminedTile);
            }
        }

        public void CollapseTile(TileType tileType)
        {
            if(IsDetermined) 
            {
                return;
            }

            mPossibleTiles.RemoveAll(x => !TileRule.IsValidNeighbour(tileType, x));
        }
    }
}
