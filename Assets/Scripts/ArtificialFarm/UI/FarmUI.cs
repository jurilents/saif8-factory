using ArtificialFarm.FarmMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace ArtificialFarm.UI
{
    public class FarmUI : MonoBehaviour
    {
        [Header("Start params")] public uint startPopulation = 20;

        [Header("Farm map")] public DisplayMode displayMode;

        public Tilemap Tilemap;
        public TileBase TilePrefab;
        public GridOrientation TileOrientation;

        [SerializeField] private Vector2Int _size = new Vector2Int(20, 16);
        [SerializeField] private bool _loopByX;
        [SerializeField] private bool _loopByY;

        [Header("Genome DNA")] public uint dnaLength = 16;

        [Header("Mutations")] [InspectorName("Chance")] [Range(0, 1f)]
        public float mutationChance = 0.1f;

        [InspectorName("Count")] [Range(0, 10)]
        public int mutationsCount = 1;

        [Header("User Interface Panel")] [SerializeField]
        private Text _stepField;

        [SerializeField] private Text _populationField;

        public RectTransform graphicContainer;

        public Size Size => new Size(_size, _loopByX, _loopByY);


        public void UpdateStepField(ulong step) => _stepField.text = $"{step}";
        public void UpdatePopField(int population) => _populationField.text = $"{population}";
    }
}