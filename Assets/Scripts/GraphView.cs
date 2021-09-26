using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use NodeViews to Represent Visually Each node from Graph Nodes data.
// Cache NodeViews

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;

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
                nodeView.ColorNode(MapData.GetColorFromNodeType(n.nodeType));
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color, bool lerpColor = false, float lerpValue = 0.5f)
    {
        foreach (Node n in nodes)
        {
            if (n!= null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                Color newColor = color;

                if (lerpColor)
                {
                    Color originalColor = MapData.GetColorFromNodeType(n.nodeType);
                    newColor = Color.Lerp(originalColor,newColor, lerpValue);
                }

                if (nodeView != null)
                {
                    nodeView.ColorNode(newColor);
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
