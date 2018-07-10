namespace Lift.Graphs
{
    public interface IEdge<V>
    {
        V From { get; }
        V To { get; }
        string Id { get; }
    }

    public interface IEdge<V,T> : IEdge<V>
    {
        T Data { get; }
    }
}
