using System.Collections.Generic;
using System.Linq;

namespace Lift.DataStructures.Graphs
{
    public class Graph : IGraph<Vertex, Edge>
    {
        private ISet<Vertex> vertices;
        private ISet<Edge> edges;

        public Graph()
        {
            vertices = new HashSet<Vertex>();
            edges = new HashSet<Edge>();
        }

        public ISet<Vertex> Vertices => vertices;
        public ISet<Edge> Edges => edges;

        public bool AddEdge(Edge edge)
        {
            if (edge == null || !vertices.Contains(edge.From) || !vertices.Contains(edge.To)) return false;

            return vertices.Where(_ => _.Equals(edge.From)).Single().AdjacentVertices.Add(edge.To) &&
            edges.Add(edge);
        }

        public bool AddVertex(Vertex vertex)
        {
            bool wasAdded = vertices.Add(vertex);

            if (wasAdded)
            {
                foreach (var neighbor in vertex.AdjacentVertices ?? Enumerable.Empty<Vertex>())
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
