namespace Lift.DataStructures.Graphs
{
    public interface IEdge<TVertex>
    {
        TVertex From { get; }
        TVertex To { get; }
    }

    public interface IEdge<TVertex,TData> : IEdge<TVertex>
    {
        TData Data { get; }
    }
}
