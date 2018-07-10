using System;

namespace Lift.Graphs
{
    public class Edge : IEdge<Vertex>, IEquatable<Edge>
    {
        private readonly Vertex from;
        private readonly Vertex to;

        public Edge(Vertex from, Vertex to)
        {
            this.from = from ?? throw new ArgumentNullException(nameof(from));
            this.to = to ?? throw new ArgumentNullException(nameof(to));
        }

        public Vertex From => from;
        public Vertex To => to;

        public string Id => from.Id + to.Id;

        public bool Equals(Edge other)
        {
            return (this.to.Equals(other.to) && this.from.Equals(other.from));
        }
    }
}
