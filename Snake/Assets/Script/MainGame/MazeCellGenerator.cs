using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellGenerator : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    [SerializeField]
    GameObject blockPrefab;

    [SerializeField]
    int numX = 10;
    [SerializeField]
    int numY = 10;

    float blockSize = 0.25f;

    GameObject[,] blocks;
    bool[,] visited;
    Stack<Vector2Int> stack = new Stack<Vector2Int>();
    bool generating = false;

    void Start()
    {
        CreateGrid();
        CreateMaze();
    }

    public void CreateGrid()
    {
        int width = numX * 2 + 1;
        int height = numY * 2 + 1;

        blocks = new GameObject[width, height];
        visited = new bool[numX, numY];

        float offsetX = (width - 1) / 2f * blockSize;
        float offsetY = (height - 1) / 2f * blockSize;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 localPos = new Vector3(x * blockSize - offsetX, y * blockSize - offsetY, 0);
                Vector3 pos = transform.position + transform.rotation * localPos;

                if (x % 2 == 1 && y % 2 == 1)
                {
                    blocks[x, y] = null;
                }
                else
                {
                    GameObject block = Instantiate(blockPrefab, pos, transform.rotation, transform);
                    block.name = $"Block_{x}_{y}";
                    blocks[x, y] = block;
                }
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbours(int x, int y)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        if (x > 0 && !visited[x - 1, y]) neighbours.Add(new Vector2Int(x - 1, y));
        if (x < numX - 1 && !visited[x + 1, y]) neighbours.Add(new Vector2Int(x + 1, y));
        if (y > 0 && !visited[x, y - 1]) neighbours.Add(new Vector2Int(x, y - 1));
        if (y < numY - 1 && !visited[x, y + 1]) neighbours.Add(new Vector2Int(x, y + 1));
        return neighbours;
    }

    public void CreateMaze()
    {
        if (generating) return;

        for (int x = 0; x < numX; x++)
            for (int y = 0; y < numY; y++)
                visited[x, y] = false;

        stack.Clear();

        Vector2Int current = new Vector2Int(0, 0);
        visited[0, 0] = true;
        stack.Push(current);

        StartCoroutine(GenerateCoroutine());
    }

    IEnumerator GenerateCoroutine()
    {
        generating = true;

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            var neighbours = GetUnvisitedNeighbours(current.x, current.y);

            if (neighbours.Count > 0)
            {
                Vector2Int chosen = neighbours[Random.Range(0, neighbours.Count)];
                visited[chosen.x, chosen.y] = true;
                RemoveWallBetween(current, chosen);
                stack.Push(chosen);
            }
            else
            {
                stack.Pop();
            }

            yield return new WaitForSeconds(0.01f);
        }

        ApplyDifficultyTweaks();

        generating = false;
    }

    void RemoveWallBetween(Vector2Int a, Vector2Int b)
    {
        int wallX = a.x + b.x + 1;
        int wallY = a.y + b.y + 1;

        if (blocks[wallX, wallY] != null)
        {
            Destroy(blocks[wallX, wallY]);
            blocks[wallX, wallY] = null;
        }
    }

    void ApplyDifficultyTweaks()
    {
        DifficultyLevel level = ParseDifficulty(My_Text.Difficult);

        int additionalWallsToRemove = 0;

        switch (level)
        {
            case DifficultyLevel.Medium:
                additionalWallsToRemove = (int)(numX * numY * 0.25f);
                break;
            case DifficultyLevel.Easy:
                additionalWallsToRemove = (int)(numX * numY * 0.5f);
                break;
            case DifficultyLevel.Hard:
            default:
                return;
        }

        int removedCount = 0;

        for (int i = 0; i < additionalWallsToRemove * 4; i++)
        {
            Vector2Int a = new Vector2Int(Random.Range(0, numX), Random.Range(0, numY));
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if (a.x > 0) neighbors.Add(new Vector2Int(a.x - 1, a.y));
            if (a.x < numX - 1) neighbors.Add(new Vector2Int(a.x + 1, a.y));
            if (a.y > 0) neighbors.Add(new Vector2Int(a.x, a.y - 1));
            if (a.y < numY - 1) neighbors.Add(new Vector2Int(a.x, a.y + 1));

            if (neighbors.Count > 0)
            {
                Vector2Int b = neighbors[Random.Range(0, neighbors.Count)];

                int wallX = a.x + b.x + 1;
                int wallY = a.y + b.y + 1;

                if (blocks[wallX, wallY] != null)
                {
                    Destroy(blocks[wallX, wallY]);
                    blocks[wallX, wallY] = null;
                    removedCount++;

                    if (removedCount >= additionalWallsToRemove)
                        break;
                }
            }
        }

    }

    DifficultyLevel ParseDifficulty(string diff)
    {
        switch (diff.ToLower())
        {
            case "easy": return DifficultyLevel.Easy;
            case "medium": return DifficultyLevel.Medium;
            case "hard": return DifficultyLevel.Hard;
            default: return DifficultyLevel.Hard;
        }
    }
}
