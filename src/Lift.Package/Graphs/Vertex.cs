using System;
using System.Collections.Generic;

namespace Lift.Graphs
{
    public class Vertex : IVertex<Vertex>
    {
        private readonly string id;
        private ISet<Vertex> adjacentVerticies;

        public Vertex(string id)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            this.adjacentVerticies = new HashSet<Vertex>();
        }

        public Vertex(string id, ISet<Vertex> adjacentVerticies):this(id)
        {
            this.adjacentVerticies = adjacentVerticies ?? throw new ArgumentNullException(nameof(adjacentVerticies));
        }

        public string Id => id;

        public ISet<Vertex> AdjacentVerticies => adjacentVerticies;

        public bool Equals(Vertex other)
        {
            return id.Equals(other.id);
        }
    }
}
