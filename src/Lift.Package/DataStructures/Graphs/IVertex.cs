using System;
using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public interface IVertex<V> : IEquatable<V>
    {
        string Id { get; }
        ISet<V> AdjacentVertices { get; }
    }

    public interface IVertex<V,T> : IVertex<V>
    {
        T Data { get; }
    }
}
