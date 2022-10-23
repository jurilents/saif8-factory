using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    public class ToolsMenuItems
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}