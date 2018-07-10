using System;
using System.Collections.Generic;

namespace Lift.Graphs
{
    public interface IVertex<V> : IEquatable<V>
    {
        string Id { get; }
        ISet<V> AdjacentVerticies { get; }
    }

    public interface IVertex<V,T> : IVertex<V>
    {
        T Data { get; }
    }
}
