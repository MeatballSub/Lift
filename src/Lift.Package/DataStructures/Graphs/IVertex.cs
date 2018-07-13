using System;
using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public interface IVertex<TVertex> : IEquatable<TVertex>
    {
        string Id { get; }
        ISet<TVertex> AdjacentVertices { get; }
    }

    public interface IVertex<TVertex,TData> : IVertex<TVertex>
    {
        TData Data { get; }
    }
}
