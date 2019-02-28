public struct NodeRecord
{
    public NavSurface node;
    public Connection connection;
    public float costSoFar;

    public NodeRecord(NavSurface n, Connection connect, float cost)
    {
        node = n;
        connection = connect;
        costSoFar = cost;
    }
}
