using Lift.Graphs;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using System;

namespace Lift.Tests.Graphs
{
    public class TopologicalSortTests
    {
        [Fact]
        public void TopologicalSortShouldWork()
        {
            {
                var graph = new Graph();

                var a = new Vertex("a");
                var b = new Vertex("b");

                graph.AddVertex(a);
                graph.AddVertex(b);

                graph.AddEdge(new Edge(a, b));

                var expected = new List<Vertex> { b, a };

                var sorter = new TopologicalSort<Vertex, Edge>();
                var sort = sorter.Sort(graph);

                Assert.True(sort.SequenceEqual(expected));
            }

            {
                var graph = new Graph();

                var a = new Vertex("a");
                var b = new Vertex("b");

                graph.AddVertex(a);
                graph.AddVertex(b);

                graph.AddEdge(new Edge(b, a));

                var expected = new List<Vertex> { a, b };

                var sorter = new TopologicalSort<Vertex, Edge>();
                var sort = sorter.Sort(graph);

                Assert.True(sort.SequenceEqual(expected));
            }

        }

        [Fact]
        public void CircularDependencyShouldBeDetected()
        {
            Graph graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");

            graph.AddVertex(a);
            graph.AddVertex(b);

            graph.AddEdge(new Edge(a, b));
            graph.AddEdge(new Edge(b, a));

            var sorter = new TopologicalSort<Vertex, Edge>();

            CircularDependencyException ex = Assert.Throws<CircularDependencyException>(() => sorter.Sort(graph));

            string expectedExceptionMsg = "Circular Dependency" + Environment.NewLine + "  b" + Environment.NewLine + "  a";

            Assert.Equal(expectedExceptionMsg, ex.Message);
        }
    }
}
