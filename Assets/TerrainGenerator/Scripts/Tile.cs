using UnityEngine;

namespace TileMapGenerator 
{
    public enum TileType 
    {
        None, 
        Grass,
        Sand,
        Dirt,
        Rock,
        Snow,
        Forest,
        SnowForest,
        Water
    }

    public class Tile : MonoBehaviour
    {
        public TileType TileType;
    }
}
