using System.Collections.Generic;
using System.Linq;

namespace Lift.Graphs
{
    public class Graph : IGraph<Vertex, Edge>
    {
        private ISet<Vertex> verticies;
        private ISet<Edge> edges;

        public Graph()
        {
            verticies = new HashSet<Vertex>();
            edges = new HashSet<Edge>();
        }

        public ISet<Vertex> Verticies => verticies;
        public ISet<Edge> Edges => edges;

        public bool AddEdge(Edge edge)
        {
            if (edge == null || !verticies.Contains(edge.From) || !verticies.Contains(edge.To)) return false;

            return verticies.Where(_ => _.Equals(edge.From)).Single().AdjacentVerticies.Add(edge.To) &&
            edges.Add(edge);
        }

        public bool AddVertex(Vertex vertex)
        {
            bool wasAdded = verticies.Add(vertex);

            if (wasAdded)
            {
                foreach (var neighbor in vertex.AdjacentVerticies ?? Enumerable.Empty<Vertex>())
                {
                    wasAdded = (verticies.Contains(neighbor))
                        ? (edges.Add(new Edge(vertex, neighbor)) && wasAdded)
                        : false;
                }
            }

            return wasAdded;
        } 
    }
}
