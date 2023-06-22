using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX, gridSizeY;
    public float nodeSizeX;
    public float nodeSizeY;
    public LayerMask unwalkableMask;
    public GameObject gridOffset;
    private Node[,] grid;

    private void Awake()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector2 worldPosition = new Vector2(i * nodeSizeX + gridOffset.transform.position.x, j * nodeSizeY + gridOffset.transform.position.y);
                bool walkable = !Physics2D.OverlapCircle(worldPosition, nodeSizeX / 2, unwalkableMask);
                Node nodeData = new Node(walkable, i, j);
                grid[i, j] = nodeData;
            }
        }
    }
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x - gridOffset.transform.position.x) / (nodeSizeX * gridSizeX);
        float percentY = (worldPosition.y - gridOffset.transform.position.y) / (nodeSizeY * gridSizeY);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
    public Vector3 WorldPointFromNode(Node node)
    {
        float worldWidth = gridSizeX * nodeSizeX;
        float worldHeight = gridSizeY * nodeSizeY;
        Vector3 gridWorldSize = new Vector3(worldWidth, worldHeight, 1f);
        float percentX = node.gridX / (float)(gridSizeX - 1);
        float percentY = node.gridY / (float)(gridSizeY - 1);
        float x = Mathf.Lerp(-gridWorldSize.x / 2f, gridWorldSize.x / 2f, percentX);
        float y = Mathf.Lerp(-gridWorldSize.y / 2f, gridWorldSize.y / 2f, percentY);
        return new Vector3(x, y, 1f);
    }
}
