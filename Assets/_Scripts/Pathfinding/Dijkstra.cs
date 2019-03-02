using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public Path PathfindDijkstra(Graph graph, NavSurface startNode, NavSurface endNode)
    {
        // Init start node
        NodeRecord startRecord = new NodeRecord(startNode, null, 0.0f);

        // Init open and close lists
        List<NodeRecord> openNodes = new List<NodeRecord>();
        List<NodeRecord> closedNodes = new List<NodeRecord>();

        // Add startRecord to open list
        openNodes.Add(startRecord);

        NodeRecord current = new NodeRecord(null, null, 0.0f);
        while (openNodes.Count > 0)
        {
            // Find smallest element in the open list
            current = FindSmallestElement(openNodes);

            // Could not find any node to work with
            if (current.node == null)
                break;

            // If current node is goal node, terminate
            if (current.node == endNode)
                break;

            // Main Search Loop ~~
            NavSurface nextNode;
            float nextNodeCost = 0.0f;
            foreach (Connection connection in current.node.connections)
            {
                // Update total cost estimate
                nextNode = connection.toNode;
                nextNodeCost += connection.cost;

                // Skip to next node if this one is already closed
                if (closedNodes.Exists(x => x.node == nextNode))
                    continue;

                NodeRecord recordedNode;
                bool nodeAlreadyExists = false;
                // Check if we already have the node with a worse cost
                if (openNodes.Exists(x => x.node == nextNode))
                {
                    nodeAlreadyExists = true;
                    recordedNode = openNodes.Find(x => x.node == nextNode);
                    if (recordedNode.costSoFar <= nextNodeCost)
                        continue;
                }
                else
                {
                    // Record the node otherwise
                    recordedNode = new NodeRecord
                    {
                        node = nextNode
                    };
                }
                recordedNode.costSoFar = nextNodeCost;
                recordedNode.connection = connection;
                recordedNode.fromNodeInPath = current;
                // Add if node doest exist, is already up to date otherwise
                if (nodeAlreadyExists == false)
                    openNodes.Add(recordedNode);
            }

            // Remove the current node from open list and add to closed
            openNodes.Remove(current);
            closedNodes.Add(current);
        }

        if (current.node != endNode)
            return null;

        // Create a path list and return
        Path path = new Path();
        while (current.node != startNode)
        {
            path.connections.Add(current.connection);
            current = current.fromNodeInPath;
        }
        path.connections.Reverse();
        return path;
    }

    private NodeRecord FindSmallestElement(List<NodeRecord> list)
    {
        NodeRecord smallest = list[0];
        foreach (NodeRecord node in list)
        {
            if (node.costSoFar < smallest.costSoFar)
                smallest = node;
        }
        return smallest;
    }
}
