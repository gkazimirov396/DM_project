using DM_project;
using System.Diagnostics;


public class Program
{
    public static void WriteToFile(string data, string algorithm, string graphType)
    {
        var path = $"D:\\DM_project_results\\timedata_{algorithm}_{graphType}.tsv";
        using (StreamWriter writer = new StreamWriter(path, append: true)) 
        {
            writer.WriteLine(data);
        }
    }
    public static bool[] BFS_adjmatrix(int vertex, int[,] matrix, int vertices)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[vertex] = true;
        queue.Enqueue(vertex);

        while (queue.Count != 0)
        {
            vertex = queue.Dequeue();

            for (int i = 0; i < vertices; ++i)
            {
                if (matrix[vertex, i] == 1 && !visited[i])
                {
                    visited[i] = true;
                    queue.Enqueue(i);
                }
            }
        }
        return visited;
    }
    public static bool[] DFS_adjmatrix(int vertex, int[,] matrix, int vertices)
    {
        bool[] visited = new bool[vertices];
        Stack<int> stack = new Stack<int>();

        stack.Push(vertex);

        while (stack.Count != 0)
        {
            vertex = stack.Pop();

            if (!visited[vertex])
            {
                visited[vertex] = true;

                for (int i = 0; i < vertices; ++i)
                {
                    if (matrix[vertex, i] == 1 && !visited[i])
                    {
                        stack.Push(i);
                    }
                }
            }
        }
        return visited;
    }
    public static void Main(string[] args)
    {
        int minVertices = int.Parse(args[0]);
        int maxVertices = int.Parse(args[1]);
        int verticesDiff = int.Parse(args[2]);
        int testsAmount = int.Parse(args[3])+ 1;
        double density = 0.2;
        while (minVertices < maxVertices)
        {
            Graph[] adjacencyListGraphs = new Graph[testsAmount];
            int[][,] adjacencyMatrixGraphs = new int[testsAmount][,];
            for (int i = 0; i < testsAmount; i++)
            {
                adjacencyListGraphs[i] = Graph.GenerateRandomGraph(minVertices, density);
                adjacencyMatrixGraphs[i] = (adjacencyListGraphs[i]).GetAdjacencyMatrix();

            }
            List<long>[] iterationTimeBFS = { new List<long>(), new List<long>() };
            List<long>[] iterationTimeDFS = { new List<long>(), new List<long>() };
            for (int i = 0; i < testsAmount; i++)
            {
                Graph graphAsList = adjacencyListGraphs[i];
                int vertices = graphAsList.vertices;
                int[,] graphAsMatrix = adjacencyMatrixGraphs[i];

                var listTimerBFS = System.Diagnostics.Stopwatch.StartNew();
                graphAsList.BFS_adjlist(1);
                listTimerBFS.Stop();
                iterationTimeBFS[0].Add(listTimerBFS.ElapsedMilliseconds);

                var matrixTimerBFS = System.Diagnostics.Stopwatch.StartNew();
                BFS_adjmatrix(1, graphAsMatrix, vertices);
                listTimerBFS.Stop();
                iterationTimeBFS[1].Add(matrixTimerBFS.ElapsedMilliseconds);

                var listTimerDFS = System.Diagnostics.Stopwatch.StartNew();
                graphAsList.DFS_adjlist(1);
                listTimerBFS.Stop();
                iterationTimeDFS[0].Add(listTimerDFS.ElapsedMilliseconds);

                var matrixTimerDFS = System.Diagnostics.Stopwatch.StartNew();
                DFS_adjmatrix(1, graphAsMatrix, vertices);
                listTimerBFS.Stop();
                iterationTimeDFS[1].Add(matrixTimerDFS.ElapsedMilliseconds);
            }
            long listBFSTimeSum = 0;
            for (int n = 0; n < iterationTimeBFS[0].Count(); n++)
            {
                listBFSTimeSum += iterationTimeBFS[0][n];
            }
            long matrixBFSTimeSum = 0;

            for (int n = 0; n < iterationTimeBFS[1].Count(); n++)
            {
                matrixBFSTimeSum += iterationTimeBFS[1][n];
            }
            long listDFSTimeSum = 0;

            for (int n = 0; n < iterationTimeDFS[0].Count(); n++)
            {
                listDFSTimeSum += iterationTimeDFS[0][n];
            }
            long matrixDFSTimeSum = 0;

            for (int n = 0; n < iterationTimeDFS[1].Count(); n++)
            {
                matrixDFSTimeSum += iterationTimeDFS[1][n];
            }

            var bfsListAvgTime = $"{minVertices}_{density.ToString("0.0")}_{listBFSTimeSum / iterationTimeBFS[0].Count}";
            var bfsMatrixAvgTime = $"{minVertices}_{density.ToString("0.0")}_{matrixBFSTimeSum / iterationTimeBFS[1].Count}";
            var dfsListAvgTime = $"{minVertices}_{density.ToString("0.0")}_{listDFSTimeSum / iterationTimeDFS[0].Count}";
            var dfsMatrixAvgTime = $"{minVertices}_{density.ToString("0.0")}_{matrixDFSTimeSum / iterationTimeDFS[1].Count}";

            WriteToFile(bfsListAvgTime, "BFS", "List");
            WriteToFile(bfsMatrixAvgTime, "BFS", "Matrix");
            WriteToFile(dfsListAvgTime, "DFS", "List");
            WriteToFile(dfsMatrixAvgTime, "DFS", "Matrix");

            if (density < 1)
            {
                density += 0.2;
            }
            else
            {
                density = 0.2;
                minVertices += verticesDiff;
            }
        }
    }
}     