using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public interface IGraph<V,E> where V : IVertex<V> where E : IEdge<V>
    {
        ISet<V> Vertices { get; }
        ISet<E> Edges { get; }
        bool AddVertex(V vertex);
        bool AddEdge(E edge);
    }
}
