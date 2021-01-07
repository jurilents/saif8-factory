using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.GraphTools
{
    public class GraphPanel
    {
        private const float BASE_OFFSET = 0.5f;

        private readonly List<LineRenderer> _lines;
        private readonly float _maxY;

        public GraphPanel(GameObject wrepper, int maxY, int linesCount = 1)
        {
            _lines = new List<LineRenderer>(linesCount);
            _lines.AddRange(wrepper.GetComponentsInChildren<LineRenderer>());

            _maxY = maxY;
        }


        public void SetLineColor(Color color, int lineId = 0)
        {
            _lines[lineId].startColor = color;
            _lines[lineId].endColor = color;
        }

        public void AddPointsRange(IReadOnlyCollection<float> points, int lineId = 0)
        {
            _lines[lineId].positionCount = points.Count;
            var vectors = points.Select((p, i) => PointToVector(i, p)).ToArray();
            _lines[lineId].SetPositions(vectors);
        }

        private Vector3 PointToVector(int position, float value) =>
            new Vector3(BASE_OFFSET * position, value / _maxY * 100f, 0);
    }
}