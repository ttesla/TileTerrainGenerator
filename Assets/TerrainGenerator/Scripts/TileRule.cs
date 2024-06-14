using System.Collections.Generic;

namespace TileMapGenerator 
{
    public static class TileRule
    {
        private static Dictionary<TileType, HashSet<TileType>> ruleSet = new();

        static TileRule()
        {
            SetupRules();
        }

        public static bool IsValidNeighbour(TileType currentTile, TileType nextTile)
        {
            bool canBeNext = false;

            if (ruleSet.TryGetValue(currentTile, out HashSet<TileType> result))
            {
                canBeNext = result.Contains(nextTile);
            }

            return canBeNext;
        }

        private static void SetupRules()
        {
            ruleSet.Add(TileType.Grass, new HashSet<TileType>() 
            { 
                TileType.Grass, 
                TileType.Dirt, 
                TileType.Rock,
                TileType.Forest
            });

            ruleSet.Add(TileType.Sand, new HashSet<TileType>()
            {
                TileType.Sand,
                TileType.Dirt,
                TileType.Water,
            });

            ruleSet.Add(TileType.Dirt, new HashSet<TileType>()
            {
                TileType.Dirt,
                TileType.Sand,
                TileType.Grass,
            });

            ruleSet.Add(TileType.Rock, new HashSet<TileType>()
            {
                TileType.Rock,
                TileType.Grass,
            });

            ruleSet.Add(TileType.Snow, new HashSet<TileType>()
            {
                TileType.Snow,
                TileType.SnowForest,
            });

            ruleSet.Add(TileType.Forest, new HashSet<TileType>()
            {
                TileType.Forest,
                TileType.SnowForest,
                TileType.Grass,
            });

            ruleSet.Add(TileType.SnowForest, new HashSet<TileType>()
            {
                TileType.SnowForest,
                TileType.Snow,
                TileType.Forest
            });

            ruleSet.Add(TileType.Water, new HashSet<TileType>()
            {
                TileType.Water,
                TileType.Sand,
            });
        }
    }
}
