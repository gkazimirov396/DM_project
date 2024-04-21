namespace DM_project;


internal class Graph
{
    private int vertices;
    private List<int>[] adjacencyList;

    public Graph(int v)
    {
        vertices = v;
        adjacencyList = new List<int>[v];

        for (int i = 0; i < v; ++i)
        {
            adjacencyList[i] = new List<int>();
        }
    }
    public static Graph GenerateRandomGraph(int size, double density)
    {
        Random random = new Random();
        Graph graph = new Graph(size);

        int edgesToAdd = (int)(size * (size - 1) * density / 2);

        while (edgesToAdd > 0)
        {
            int u = random.Next(0, size);
            int v = random.Next(0, size);

            if (u != v && !graph.adjacencyList[u].Contains(v))
            {
                graph.AddEdge(u, v);
                edgesToAdd--;
            }
        }

        return graph;
    }

    public void AddEdge(int u, int v)
    {
        adjacencyList[u].Add(v);
        adjacencyList[v].Add(u);
    }

    public void RemoveEdge(int u, int v)
    {
        adjacencyList[u].Remove(v);
        adjacencyList[v].Remove(u);
    }

    public void PrintAdjacencyList()
    {
        for (int i = 0; i < vertices; ++i)
        {
            Console.Write($"Vertex {i}: ");

            foreach (int vertex in adjacencyList[i])
            {
                Console.Write($"{vertex} ");
            }

            Console.WriteLine();
        }
    }

    public void PrintAdjacencyMatrix()
    {
        int[,] adjacencyMatrix = GetAdjacencyMatrix();

        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                Console.Write($"{adjacencyMatrix[i, j]} ");
            }

            Console.WriteLine();
        }
    }

    private int[,] GetAdjacencyMatrix()
    {
        int[,] matrix = new int[vertices, vertices];

        for (int i = 0; i < vertices; ++i)
        {
            foreach (int vertex in adjacencyList[i])
            {
                matrix[i, vertex] = 1;
            }
        }

        return matrix;
    }
    public bool[] BFS_adjlist(int vertex)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[vertex] = true;
        queue.Enqueue(vertex);

        while (queue.Count != 0)
        {
            vertex = queue.Dequeue();
            foreach (int i in adjacencyList[vertex])
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    queue.Enqueue(i);
                }
            }
        }
        return visited;
    }
    public bool[] BFS_adjmatrix(int vertex, int[,] matrix)
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
    public bool[] DFS_adjlist(int vertex)
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
            }
            foreach (int i in adjacencyList[vertex])
            {
                if (!visited[i])
                {
                    stack.Push(i);
                }
            }
        }
        return visited;
    }
    public bool[] DFS_adjmatrix(int vertex, int[,] matrix)
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
}

