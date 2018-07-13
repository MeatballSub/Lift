using System.Collections.Generic;
using System.Linq;

namespace Lift.DataStructures.Graphs
{
    public class Graph : IGraph<Vertex, Edge>
    {
        private readonly ISet<Vertex> vertices;
        private readonly ISet<Edge> edges;

        public Graph()
        {
            vertices = new HashSet<Vertex>();
            edges = new HashSet<Edge>();
        }

        public ISet<Vertex> Vertices => vertices;
        public ISet<Edge> Edges => edges;

        public bool AddEdge(Edge edge)
        {
            if (edge == null || !vertices.Contains(edge.To)) return false;

            var vertex = vertices.FirstOrDefault(_ => _.Equals(edge.From));
            if (vertex == null) return false;

            return vertex.AdjacentVertices.Add(edge.To) && edges.Add(edge);
        }

        public bool AddVertex(Vertex vertex)
        {
            bool wasAdded = vertex != null && vertices.Add(vertex);

            if (wasAdded)
            {
                foreach (var neighbor in vertex.AdjacentVertices)
                {
                    wasAdded = (vertices.Contains(neighbor))
                        ? (edges.Add(new Edge(vertex, neighbor)) && wasAdded)
                        : false;
                }
            }

            return wasAdded;
        } 
    }
}
