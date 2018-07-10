using Lift.Graphs;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lift.Tests.Graphs
{
    public class GraphTests
    {
        [Fact]
        public void AddVertexWorks()
        {
            {
                var graph = new Graph();

                Assert.True(graph.AddVertex(new Vertex("a")));
                Assert.True(graph.AddVertex(new Vertex("b")));
                Assert.True(graph.AddVertex(new Vertex("c")));
                Assert.True(graph.AddVertex(new Vertex("d")));

                var expectedIds = new List<string> { "a", "b", "c", "d" };
                var expectedAdjCounts = new List<int> { 0, 0, 0, 0 };

                Assert.Equal(expectedIds.Count, expectedAdjCounts.Count);
                Assert.Equal(4, graph.Verticies.Count);
                Assert.Equal(0, graph.Edges.Count);

                for (int i = 0; i < expectedIds.Count; ++i)
                {
                    string id = expectedIds[i];
                    int adjCount = expectedAdjCounts[i];
                    Vertex vertex = graph.Verticies.Where(_ => _.Id.Equals(id)).Single();
                    Assert.Equal(adjCount, vertex.AdjacentVerticies.Count);
                }
            }

            {
                var graph = new Graph();

                var a = new Vertex("a");
                var b = new Vertex("b");
                var c = new Vertex("c", new HashSet<Vertex> { a, b });
                var d = new Vertex("d", new HashSet<Vertex> { c });

                Assert.True(graph.AddVertex(a));
                Assert.True(graph.AddVertex(b));
                Assert.True(graph.AddVertex(c));
                Assert.True(graph.AddVertex(d));

                var expectedIds = new List<string> { "a", "b", "c", "d" };
                var expectedAdjCounts = new List<int> { 0, 0, 2, 1 };
                var expectedCAdj = new List<string> { "a", "b" };
                var expectedDAdj = new List<string> { "c" };

                Assert.Equal(expectedIds.Count, expectedAdjCounts.Count);
                Assert.Equal(4, graph.Verticies.Count);
                Assert.Equal(3, graph.Edges.Count);

                for (int i = 0; i < expectedIds.Count; ++i)
                {
                    string id = expectedIds[i];
                    int adjCount = expectedAdjCounts[i];
                    Vertex vertex = graph.Verticies.Where(_ => _.Id.Equals(id)).Single();
                    Assert.Equal(adjCount, vertex.AdjacentVerticies.Count);
                }

                foreach (string adj in expectedCAdj)
                {
                    Vertex vertex = c.AdjacentVerticies.Where(_ => _.Id.Equals(adj)).Single();
                }

                foreach (string adj in expectedDAdj)
                {
                    Vertex vertex = d.AdjacentVerticies.Where(_ => _.Id.Equals(adj)).Single();
                }
            }

        }

        [Fact]
        public void AddEdgeWorks()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");
            var c = new Vertex("c");
            var d = new Vertex("d");

            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);

            var edge1 = new Edge(c, a);
            var edge2 = new Edge(c, b);
            var edge3 = new Edge(d, c);

            graph.AddEdge(edge1);
            graph.AddEdge(edge2);
            graph.AddEdge(edge3);

            var expectedIds = new List<string> { "a", "b", "c", "d" };
            var expectedAdjCounts = new List<int> { 0, 0, 2, 1 };
            var expectedCAdj = new List<string> { "a", "b" };
            var expectedDAdj = new List<string> { "c" };

            Assert.Equal(expectedIds.Count, expectedAdjCounts.Count);
            Assert.Equal(4, graph.Verticies.Count);
            Assert.Equal(3, graph.Edges.Count);

            for (int i = 0; i < expectedIds.Count; ++i)
            {
                string id = expectedIds[i];
                int adjCount = expectedAdjCounts[i];
                Vertex vertex = graph.Verticies.Where(_ => _.Id.Equals(id)).Single();
                Assert.Equal(adjCount, vertex.AdjacentVerticies.Count);
            }

            foreach(string adj in expectedCAdj)
            {
                Vertex vertex = c.AdjacentVerticies.Where(_ => _.Id.Equals(adj)).Single();
            }

            foreach (string adj in expectedDAdj)
            {
                Vertex vertex = d.AdjacentVerticies.Where(_ => _.Id.Equals(adj)).Single();
            }
        }
    }
}
