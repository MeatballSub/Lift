using System;

namespace Lift.DataStructures.Graphs
{
    public class Edge : IEdge<Vertex>, IEquatable<Edge>
    {
        public Edge(Vertex from, Vertex to)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
        }

        public Vertex From { get; }
        public Vertex To { get;  }

        public override string ToString() => $"{From.Id}->{To.Id}";

        public bool Equals(Edge other) => ((other != null) && To.Equals(other.To) && From.Equals(other.From));
    }
}
