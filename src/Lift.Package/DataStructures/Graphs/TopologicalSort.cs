using System.Linq;
using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public class TopologicalSort<V,E> where V : IVertex<V> where E : IEdge<V>
    {
        private ICollection<V> sortedVerticies;
        private ICollection<V> visited;
        private ICollection<V> processing;

        public TopologicalSort()
        {
            sortedVerticies = new List<V>();
            visited = new List<V>();
            processing = new List<V>();
        }

        public IEnumerable<V> Sort(IGraph<V, E> graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                Visit(vertex);
            }
            return sortedVerticies;
        }

        private void Visit(V vertex)
        {
            if (processing.Contains(vertex))
            {
                throw new CircularDependencyException();
            }

            if (visited.Contains(vertex))
            {
                return;
            }

            visited.Add(vertex);
            Process(vertex);
            sortedVerticies.Add(vertex);
        }

        private void Process(V vertex)
        {
            processing.Add(vertex);

            foreach (V dependency in vertex.AdjacentVertices ?? Enumerable.Empty<V>())
            {
                try
                {
                    Visit(dependency);
                }
                catch(CircularDependencyException ex)
                {
                    throw new CircularDependencyException(vertex.Id, ex);
                }
            }

            processing.Remove(vertex);
        }
    }
}
