using System.Collections.Generic;

namespace Lift.Graphs
{
    public interface IGraph<V,E> where V : IVertex<V> where E : IEdge<V>
    {
        ISet<V> Verticies { get; }
        ISet<E> Edges { get; }
        bool AddVertex(V vertex);
        bool AddEdge(E edge);
    }
}
