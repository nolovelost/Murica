using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Graph : MonoBehaviour
{
    // A list of all nodes holding data about their connections
    public List<NavSurface> nodes;

    public Grid grid;
    public Tilemap tilemap;
    public string lastUpdate;

    [ContextMenu("Recalculate Navigation Graph DB")]
    public void CalculateGraph()
    {
        lastUpdate = System.DateTime.Now.ToString();

        if (nodes == null)
        {
            nodes = new List<NavSurface>();
        }
        // Currently the graph is cleared on every update
        // Is tied directly into the loop, hence having to loop through all tiles everytime we update
        // #TODO Need to have it so such that only the edited nodes need to clear
        if (nodes != null)
        {
            if (nodes.Count > 0)
            {
                foreach (NavSurface node in nodes)
                {
                    ScriptableObject.DestroyImmediate(node);
                }
                nodes.Clear();
            }
        }
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            // Spawn a new node for each tile position
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace))
            {
                NavSurface nav = ScriptableObject.CreateInstance<NavSurface>();
                SmartTile tile = tilemap.GetTile<SmartTile>(pos);

                nav.gridPos = pos;
                nav.isNavigable = tile.nav.isNavigable;
                nav.cost = tile.nav.cost;
                nav.isCover = tile.nav.isCover;
                nav.coverLocations = new List<Vector3Int>();
                nav.coverValue = tile.nav.coverValue;

                // Add the new node to the nodes list
                nodes.Add(nav);
            }
        }

        // Calculating connections for all nodes
        foreach (NavSurface node in nodes)
        {
            if (node.isNavigable)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        Vector3Int neighbourPos = new Vector3Int(node.gridPos.x + i,
                            node.gridPos.y + j,
                            node.gridPos.z);
                        if (HasSmartTile(tilemap, neighbourPos))
                        {
                            //int index = nodeIndex.FindLastIndex(pos);
                            NavSurface neighbourNode = nodes.Find(x => x.gridPos == neighbourPos);
                            if (neighbourNode.isNavigable)
                            {
                                Connection connection = ScriptableObject.CreateInstance<Connection>();
                                connection.fromNode = node;
                                connection.toNode = neighbourNode;
                                node.connections.Add(connection);
                            }
                        }
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
