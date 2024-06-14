using UnityEngine;
using UnityEngine.UI;

namespace TileMapGenerator 
{
    public class TestUI : MonoBehaviour
    {
        public TerrainGenerator Generator;
        public Button GenerateButton;

        void Awake()
        {
            GenerateButton.onClick.AddListener(GenerateTerrain);
        }

        private void GenerateTerrain() 
        {
            Generator.GenerateMap();
        }
    }
}
