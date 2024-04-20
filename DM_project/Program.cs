using DM_project;

Graph g = new Graph(5);
g.AddEdge(0, 1);
g.AddEdge(0, 2);
g.AddEdge(1, 2);
g.AddEdge(1, 3);
g.AddEdge(2, 4);

Console.WriteLine("Adjacency Lists:");
g.PrintAdjacencyList();

Console.WriteLine("\nAdjacency Matrix:");
g.PrintAdjacencyMatrix();

Graph randomGraph = Graph.GenerateRandomGraph(6, 1);
Console.WriteLine("\nRandom Adjacency Lists:");
randomGraph.PrintAdjacencyList();
