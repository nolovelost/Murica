using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : ScriptableObject
{
    public NavSurface fromNode { get; set; }
    public NavSurface toNode { get; set; }


    public float GetCost()
    {
        return toNode.cost;
    }

    public float GetAverageCost()
    {
        return (fromNode.cost + toNode.cost) / 2;
    }
}
