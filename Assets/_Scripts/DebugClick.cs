using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugClick : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;

    Dictionary<Vector3Int, NavSurface> navDict = new Dictionary<Vector3Int, NavSurface>();

    void Start()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            NavSurface nav = ScriptableObject.CreateInstance<NavSurface>();
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace))
            {
                SmartTile tile = tilemap.GetTile<SmartTile>(pos);

                nav.isNavigable = tile.nav.isNavigable;
                nav.cost = tile.nav.cost;
                nav.isCover = tile.nav.isCover;
                nav.coverLocations = new List<Vector3Int>();
                nav.coverValue = tile.nav.coverValue;

                navDict.Add(pos, nav);
            }
        }

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            NavSurface nav;
            navDict.TryGetValue(pos, out nav);
            if (nav.isCover)
            {
                Debug.Log("Is Cover.");
                for (int i = -1; i <= 1; i++)
                {
                    if (i == 0)
                        continue;
                    Vector3Int positionX = new Vector3Int(pos.x + i, pos.y, pos.z);
                    Vector3Int positionY = new Vector3Int(pos.x, pos.y + i, pos.z);
                    if (HasSmartTile(tilemap, positionX))
                    {
                        if (tilemap.GetTile<SmartTile>(positionX).nav.isNavigable)
                        {
                            nav.coverLocations.Add(positionX);
                            Debug.Log("Cover Added: " + positionX);
                        }
                    }
                    if (HasSmartTile(tilemap, positionY))
                    {
                        if (tilemap.GetTile<SmartTile>(positionY).nav.isNavigable)
                        {
                            nav.coverLocations.Add(positionY);
                            Debug.Log("Cover Added: " + positionY);
                        }
                    }
                }
                navDict.Remove(pos);
                navDict.Add(pos, nav);
            }
        }
        Debug.Log("Done defining nav locations.");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = grid.WorldToCell(worldPoint);
            SmartTile tile = tilemap.GetTile<SmartTile>(position);
            NavSurface nav;

            if (navDict.ContainsKey(position))
            {
                if (navDict.TryGetValue(position, out nav))
                {
                    Debug.Log("Tile cover amount: " + nav.coverLocations.Count);
                    Debug.Log("Tile cover locations :-");
                    foreach (Vector3Int pos in nav.coverLocations)
                    {
                        Debug.Log(pos);
                    }
                }
            }
        }
    }

    private bool HasSmartTile(Tilemap tmap, Vector3Int position)
    {
        if (!tmap.GetTile(position))
            return false;

        return tmap.GetTile(position).GetType() == System.Type.GetType("SmartTile");
    }
}
