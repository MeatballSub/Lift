using System;
using System.Linq;
using System.Collections.Generic;

namespace Lift.DataStructures.Graphs
{
    public class TopologicalSort<TVertex,TEdge>
        where TVertex : IVertex<TVertex>
        where TEdge : IEdge<TVertex>
    {
        private readonly ICollection<TVertex> sortedVertices;
        private readonly ICollection<TVertex> visited;
        private readonly ICollection<TVertex> processing;

        public TopologicalSort()
        {
            sortedVertices = new List<TVertex>();
            visited = new List<TVertex>();
            processing = new List<TVertex>();
        }

        public IEnumerable<TVertex> Sort(IGraph<TVertex, TEdge> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            foreach (var vertex in graph.Vertices)
            {
                Visit(vertex);
            }
            return sortedVertices;
        }

        private void Visit(TVertex vertex)
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
            sortedVertices.Add(vertex);
        }

        private void Process(TVertex vertex)
        {
            processing.Add(vertex);

            foreach (TVertex dependency in vertex.AdjacentVertices)
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
