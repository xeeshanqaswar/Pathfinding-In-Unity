using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[,] nodes;
    public List<Node> walls = new List<Node>();

    private int[,] m_mapData;
    private int m_width;
    private int m_height;

    public static readonly Vector2[] allDirections =
    {
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(-1,1),
        new Vector2(-1,-1),
        new Vector2(0,-1),
        new Vector2(1,-1),
    };


    public void Init(int[,] mapData)
    {
        m_mapData = mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);

        nodes = new Node[m_width, m_height];
        for (int i = 0; i < m_height; i++)
        {
            for (int j = 0; j < m_width; j++)
            {
                NodeType type = (NodeType)m_mapData[i, j];
                Node newNode = new Node(i, j, type);
                newNode.position = new Vector3(i, 0, j);

                nodes[i, j] = newNode;
                if (type == NodeType.Blocked)
                {
                    walls.Add(newNode);
                }
            }
        }

        for (int i = 0; i < m_height; i++)
        {
            for (int j = 0; j < m_width; j++)
            {
                nodes[i, j].neighbours = GetNeighbours(i, j);
            }
        }
    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0 && y < m_height);
    }

    public List<Node> GetNeighbours(int x, int y, Node[,] nodeList, Vector2[] directions)
    {
        List<Node> neighbourNodes = new List<Node>();
        
        foreach (Vector2 dir in directions)
        {
            int newX = x + (int)dir.x;
            int newY = x + (int)dir.y;

            if (nodeList[newX,newY] != null && IsWithinBounds(newX,newY) && 
                nodeList[newX,newY].nodeType != NodeType.Blocked)
            {
                neighbourNodes.Add(nodeList[newX, newY]);
            }
        }

        return neighbourNodes;
    }

    public List<Node> GetNeighbours(int x, int y)
    {
        return GetNeighbours(x, y, nodes, allDirections);
    }

}
