using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

// #TODO Right now, it fulfills two functions, one for SmartTile and one for Graph - split it in two.
[System.Serializable]
public class NavSurface : ScriptableObject
{
    public Vector3Int gridPos;
    public bool isNavigable;
    public float cost;
    public bool isCover;
    public List<Vector3Int> coverLocations = new List<Vector3Int>();
    public int coverValue;
    public List<Connection> connections = new List<Connection>();


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
