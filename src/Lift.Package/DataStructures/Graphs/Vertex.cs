using System;
using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public class Vertex : IVertex<Vertex>
    {
        private readonly string id;
        private ISet<Vertex> adjacentVertices;

        public Vertex(string id)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            adjacentVertices = new HashSet<Vertex>();
        }

        public Vertex(string id, ISet<Vertex> adjacentVerticies)
            :this(id)
        {
            this.adjacentVertices = adjacentVerticies ?? throw new ArgumentNullException(nameof(adjacentVerticies));
        }

        public string Id => id;

        public ISet<Vertex> AdjacentVertices => adjacentVertices;

        public bool Equals(Vertex other)
        {
            return id.Equals(other.id);
        }
    }
}
