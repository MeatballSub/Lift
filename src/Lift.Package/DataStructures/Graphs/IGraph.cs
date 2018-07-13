using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public interface IGraph<TVertex,TEdge>
        where TVertex : IVertex<TVertex>
        where TEdge : IEdge<TVertex>
    {
        ISet<TVertex> Vertices { get; }
        ISet<TEdge> Edges { get; }
        bool AddVertex(TVertex vertex);
        bool AddEdge(TEdge edge);
    }
}
