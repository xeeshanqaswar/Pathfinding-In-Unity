using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use NodeViews to Represent Visually Each node from Graph Nodes data.
// Cache NodeViews

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;
    public Color baseColor = Color.white;
    public Color wallColor = Color.black;

    public NodeView[,] nodeViews;
    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.Log("GRAPHVIEW : graph is not defined");
            return;
        }

        nodeViews = new NodeView[graph.Width, graph.Height];
        foreach (Node n in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);

            if (instance.TryGetComponent<NodeView>(out NodeView nodeView))
            {
                nodeViews[n.xIndex, n.yIndex] = nodeView;
                nodeView.Init(n);
                if (n.nodeType == NodeType.Blocked)
                {
                    nodeView.ColorNode(wallColor);
                }
                else
                {
                    nodeView.ColorNode(baseColor);
                }
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach (Node n in nodes)
        {
            if (n!= null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                if (nodeView != null)
                {
                    nodeView.ColorNode(color);
                }
            }
        }
    }
    public void ShowNodeArrow(List<Node> nodeList, Color color)
    {
        foreach (Node node in nodeList)
        {
            ShowNodeArrow(node, color);
        }
    }

    public void ShowNodeArrow(Node n, Color color)
    {
        if (n != null)
        {
            NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
            if (nodeView != null)
            {
                nodeView.ShowArrow(color);
            }
        }
    }
    

}
