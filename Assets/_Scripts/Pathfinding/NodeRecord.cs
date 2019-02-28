[System.Serializable]
public class NodeRecord
{
    public NavSurface node;
    public NodeRecord fromNodeInPath;
    public Connection connection;
    public float costSoFar;

    public NodeRecord() { }
    public NodeRecord(NavSurface n, Connection connect, float cost)
    {
        node = n;
        fromNodeInPath = null;
        connection = connect;
        costSoFar = cost;
    }
}
