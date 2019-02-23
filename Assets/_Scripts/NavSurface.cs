using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class NavSurface : ScriptableObject
{
    public bool isNavigable;
    public int cost;
    public bool isCover;
    public List<Vector3Int> coverLocations;
    public int coverValue;

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/NavSurface")]
    public static void CreateNavSurface()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Nav Surface", "New Nav Surface", "asset", "Save Nav Surface", "NavSurface");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<NavSurface>(), path);
    }
#endif
}
