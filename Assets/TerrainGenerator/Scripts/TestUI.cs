using System;
using TileMapGenerator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI 
{
    public class TestUI : MonoBehaviour
    {
        public TerrainGenerator Generator;
        public Button GenerateButton;
        public TMP_InputField WidthField;
        public TMP_InputField HeightField;

        void Awake()
        {
            GenerateButton.onClick.AddListener(GenerateTerrain);
        }

        private void GenerateTerrain() 
        {
            int width = 50;
            int height = 50;    
            
            try 
            {
                width = Convert.ToInt32(WidthField.text.Trim());
                width = Mathf.Clamp(width, 1, 100);
            }
            catch { }

            try
            {
                height = Convert.ToInt32(HeightField.text.Trim());
                height = Mathf.Clamp(height, 1, 100);
            }
            catch { }

            WidthField.text = width.ToString();
            HeightField.text = height.ToString();

            Generator.GenerateMap(width, height);
        }
    }
}
