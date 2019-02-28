using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class DebugGraph : MonoBehaviour
{
    private Graph graph;

    public bool drawConnections = false;

    private void Start()
    {
        graph = GetComponent<Graph>();
    }

    private void Update()
    {
        if (drawConnections)
        {
            foreach (NavSurface node in graph.nodes)
            {
                if (node.isNavigable && node.connections != null)
                {
                    foreach (Connection edge in node.connections)
                    {
                        Debug.DrawLine(graph.grid.GetCellCenterWorld(node.gridPos), graph.grid.GetCellCenterWorld(edge.toNode.gridPos), Color.red);
                    }
                }
            }
        }
    }
}
