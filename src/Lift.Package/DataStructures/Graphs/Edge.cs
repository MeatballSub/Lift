using System;

namespace Lift.DataStructures.Graphs
{
    public class Edge : IEdge<Vertex>, IEquatable<Edge>
    {
        public Edge(Vertex from, Vertex to)
        {
            this.From = from ?? throw new ArgumentNullException(nameof(from));
            this.To = to ?? throw new ArgumentNullException(nameof(to));
        }

        public Vertex From { get; }
        public Vertex To { get;  }

        public string Id => From.Id + "->" + To.Id;

        public bool Equals(Edge other)
        {
            return (this.To.Equals(other.To) && this.From.Equals(other.From));
        }
    }
}
