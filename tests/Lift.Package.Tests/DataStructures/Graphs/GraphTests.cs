using Lift.DataStructures.Graphs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lift.Tests.DataStructures.Graphs
{
    public class GraphTests
    {
        [Fact]
        public void AddVertex_WhenAdded_ShouldReturnTrue()
        {
            var graph = new Graph();

            Assert.True(graph.AddVertex(new Vertex("a")));
        }

        [Fact]
        public void AddAlreadyPresentVertex_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            graph.AddVertex(a);
            Assert.False(graph.AddVertex(a));
        }

        [Fact]
        public void AddVertexWithNeighbors_WhenAdded_ShouldReturnTrue()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b", new HashSet<Vertex> { a });
            graph.AddVertex(a);
            Assert.True(graph.AddVertex(b));
        }

        [Fact]
        public void AddVertexWithNonExistentNeighbors_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");
            var c = new Vertex("c", new HashSet<Vertex> { a, b });
            graph.AddVertex(a);
            Assert.False(graph.AddVertex(c));
        }

        [Fact]
        public void AddEdge_WhenAdded_ShouldReturnTrue()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");

            graph.AddVertex(a);
            graph.AddVertex(b);

            var edge = new Edge(a, b);

            Assert.True(graph.AddEdge(edge));
        }

        [Fact]
        public void AddAlreadyPresentEdge_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");

            graph.AddVertex(a);
            graph.AddVertex(b);

            var edge = new Edge(a, b);

            graph.AddEdge(edge);
            Assert.False(graph.AddEdge(edge));
        }

        [Fact]
        public void AddNullEdge_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            Assert.False(graph.AddEdge(null));
        }

        [Fact]
        public void AddEdgeWithMissingFromVertex_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");

            graph.AddVertex(b);

            var edge = new Edge(a, b);

            Assert.False(graph.AddEdge(edge));
        }

        [Fact]
        public void AddEdgeWithMissingToVertex_WhenAdded_ShouldReturnFalse()
        {
            var graph = new Graph();

            var a = new Vertex("a");
            var b = new Vertex("b");

            graph.AddVertex(a);

            var edge = new Edge(a, b);

            Assert.False(graph.AddEdge(edge));
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddVertex_AfterAdded_VertexCountShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddVertexTestArrangeAndAct(testData);

            Assert.Equal(testData.Count, graph.Vertices.Count);
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddVertex_AfterAdded_EdgeCountShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddVertexTestArrangeAndAct(testData);

            Assert.Equal(adjacencySets.Sum(_ => _.Value.Count), graph.Edges.Count);
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddVertex_AfterAdded_GraphShouldContainVertex(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddVertexTestArrangeAndAct(testData);

            foreach (string id in testData.Keys)
            {
                Vertex vertex = graph.Vertices.Where(_ => _.Id.Equals(id)).Single();
            }
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddVertex_AfterAdded_AdjacencyCountsShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddVertexTestArrangeAndAct(testData);

            foreach (string id in testData.Keys)
            {
                Vertex vertex = graph.Vertices.Where(_ => _.Id.Equals(id)).Single();
                Assert.Equal(adjacencySets[id].Count, vertex.AdjacentVertices.Count);
            }
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddVertex_AfterAdded_AdjacencySetsShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddVertexTestArrangeAndAct(testData);

            foreach (string id in testData.Keys)
            {
                Vertex vertex = graph.Vertices.Where(_ => _.Id.Equals(id)).Single();
                Assert.True(adjacencySets[id].SetEquals(vertex.AdjacentVertices));
            }
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddEdge_AfterAdded_EdgeCountShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddEdgeTestArrangeAndAct(testData);

            Assert.Equal(adjacencySets.Sum(_ => _.Value.Count), graph.Edges.Count);
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddEdge_AfterAdded_GraphShouldContainEdge(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddEdgeTestArrangeAndAct(testData);

            foreach(var fromVertex in testData)
            {
                foreach(var toVertex in fromVertex.Value ?? Enumerable.Empty<string>())
                {
                    Edge edge = graph.Edges.Where(_ => _.ToString().Equals(fromVertex.Key + "->" + toVertex)).Single();
                }
            }
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddEdge_AfterAdded_AdjacencyCountsShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddEdgeTestArrangeAndAct(testData);

            foreach (string id in testData.Keys)
            {
                Vertex vertex = graph.Vertices.Where(_ => _.Id.Equals(id)).Single();
                Assert.Equal(adjacencySets[id].Count, vertex.AdjacentVertices.Count);
            }
        }

        [Theory]
        [ClassData(typeof(Graph_TestData))]
        public void AddEdge_AfterAdded_AdjacencySetsShouldBeCorrect(Dictionary<string, List<string>> testData)
        {
            var (graph, adjacencySets) = AddEdgeTestArrangeAndAct(testData);

            foreach (string id in testData.Keys)
            {
                Vertex vertex = graph.Vertices.Where(_ => _.Id.Equals(id)).Single();
                Assert.True(adjacencySets[id].SetEquals(vertex.AdjacentVertices));
            }
        }

        public class Graph_TestData : IEnumerable<object[]>
        {
            private readonly List<object[]> data = new List<object[]>
            {
                new object[]
                {
                    new Dictionary<string, List<string>>{ { "a", null }, { "b", null }, { "c", null }, { "d", null } },
                },
                new object[]
                {
                    new Dictionary<string, List<string>>{ { "a", null }, { "b", null }, { "c", new List<string>{"a", "b"} }, { "d", new List<string> { "c" } } },
                }
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private (Graph, Dictionary<string, Vertex>, Dictionary<string, HashSet<Vertex>>) AddVertexTestArrange(Dictionary<string, List<string>> testData)
        {
            var graph = new Graph();

            var vertexDictionary = new Dictionary<string, Vertex>();
            var adjacencySets = new Dictionary<string, HashSet<Vertex>>();

            foreach (var vertex in testData)
            {
                var neighbors = new HashSet<Vertex>();
                foreach (var neighbor in vertex.Value ?? Enumerable.Empty<string>())
                {
                    neighbors.Add(vertexDictionary[neighbor]);
                }
                vertexDictionary.Add(vertex.Key, new Vertex(vertex.Key, neighbors));
                adjacencySets.Add(vertex.Key, neighbors);
            }

            return (graph, vertexDictionary, adjacencySets);
        }

        private void AddVertexTestAct(ref Graph graph, Dictionary<string, Vertex> vertexDictionary, Dictionary<string, List<string>> testData)
        {
            foreach (string id in testData.Keys)
            {
                graph.AddVertex(vertexDictionary[id]);
            }
        }

        private (Graph, Dictionary<string, HashSet<Vertex>>) AddVertexTestArrangeAndAct(Dictionary<string, List<string>> testData)
        {
            var (graph, vertexDictionary, adjacencySets) = AddVertexTestArrange(testData);
            AddVertexTestAct(ref graph, vertexDictionary, testData);
            return (graph, adjacencySets);
        }

        private (Graph, Dictionary<string, Edge>, Dictionary<string, HashSet<Vertex>>) AddEdgeTestArrange(Dictionary<string, List<string>> testData)
        {
            var graph = new Graph();

            var vertexDictionary = new Dictionary<string, Vertex>();
            var edgeDictionary = new Dictionary<string, Edge>();
            var adjacencySets = new Dictionary<string, HashSet<Vertex>>();
            foreach (var vertex in testData)
            {
                vertexDictionary.Add(vertex.Key, new Vertex(vertex.Key));
                graph.AddVertex(vertexDictionary[vertex.Key]);
                var neighbors = new HashSet<Vertex>();
                foreach (var neighbor in vertex.Value ?? Enumerable.Empty<string>())
                {
                    neighbors.Add(vertexDictionary[neighbor]);
                    edgeDictionary.Add(vertex.Key + "->" + neighbor, new Edge(vertexDictionary[vertex.Key], vertexDictionary[neighbor]));
                }
                adjacencySets.Add(vertex.Key, neighbors);
            }

            return (graph, edgeDictionary, adjacencySets);
        }

        private void AddEdgeTestAct(ref Graph graph, Dictionary<string, Edge> edgeDictionary, Dictionary<string, List<string>> testData)
        {
            foreach (var vertex in testData)
            {
                foreach (var neighbor in vertex.Value ?? Enumerable.Empty<string>())
                {
                    graph.AddEdge(edgeDictionary[vertex.Key + "->" + neighbor]);
                }
            }
        }

        private (Graph, Dictionary<string, HashSet<Vertex>>) AddEdgeTestArrangeAndAct(Dictionary<string, List<string>> testData)
        {
            var (graph, edgeDictionary, adjacencySets) = AddEdgeTestArrange(testData);
            AddEdgeTestAct(ref graph, edgeDictionary, testData);
            return (graph, adjacencySets);
        }
    }
}
