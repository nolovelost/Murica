using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class DebugGraph : MonoBehaviour
{
    private Graph graph;
    public NavSurface startNode;
    public NavSurface goalNode;
    public Path path;

    public bool drawConnections = false;
    public bool drawPath = false;

    private void Start()
    {
        graph = GetComponent<Graph>();
    }

    private void Update()
    {
        // DRAW stuff
        if (drawConnections)
        {
            foreach (NavSurface node in graph.nodes)
            {
                if (node.isNavigable && node.connections != null)
                {
                    foreach (Connection edge in node.connections)
                    {
                        Debug.DrawLine(graph.grid.GetCellCenterWorld(node.gridPos),
                            graph.grid.GetCellCenterWorld(edge.toNode.gridPos),
                            Color.red);
                    }
                }
            }
        }
        if (drawPath)
        {
            if (path != null)
            {
                foreach (Connection connection in path.connections)
                {
                    Debug.DrawLine(graph.grid.GetCellCenterWorld(connection.fromNode.gridPos),
                        graph.grid.GetCellCenterWorld(connection.toNode.gridPos),
                        Color.blue);
                }
            }
        }

        // INTERACTABLE stuff
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = graph.grid.WorldToCell(worldPoint);
            startNode = graph.nodes.Find(x => x.gridPos == position);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = graph.grid.WorldToCell(worldPoint);
            goalNode = graph.nodes.Find(x => x.gridPos == position);
        }
    }

    [ContextMenu("Calculate path")]
    void CalculatePath()
    {
        Dijkstra dijkstra = new Dijkstra();
        if (startNode != null && goalNode != null)
        {
            path = dijkstra.PathfindDijkstra(graph, startNode, goalNode);
        }
    }
}
