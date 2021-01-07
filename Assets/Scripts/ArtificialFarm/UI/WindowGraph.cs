using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ArtificialFarm.UI
{
    public class WindowGraph : MonoBehaviour
    {
        [SerializeField] private Sprite circleSprite;
        [SerializeField] private RectTransform graphContainer;

        private GameObject CreateCircle(Vector2 anchoredPosition)
        {
            var obj = new GameObject("circle", typeof(Image));
            obj.transform.SetParent(graphContainer, false);
            obj.GetComponent<Image>().sprite = circleSprite;
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return obj;
        }

        public void ShowGraph(IReadOnlyList<int> valueList, float yMaximum = 100f, float xSize = 50f)
        {
            float graphHeight = graphContainer.sizeDelta.y;

            Debug.Log("count: " + valueList.Count);
            Debug.Log(yMaximum + " gh " + xSize);

            GameObject lastCircleGameObject = null;
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;

                Debug.LogWarning($"x:{xPosition} y:{yPosition}");

                var circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                        circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }

                lastCircleGameObject = circleGameObject;
            }
        }

        private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
        {
            var obj = new GameObject("dotConnection", typeof(Image));
            obj.transform.SetParent(graphContainer, false);
            obj.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
            var rectTransform = obj.GetComponent<RectTransform>();
            var dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        }
    }
}