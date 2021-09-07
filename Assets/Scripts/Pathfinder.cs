using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    private Node m_startNode;
    private Node m_goalNode;
    private Graph m_graph;
    private GraphView m_graphView;

    private Queue<Node> m_frontierNodes;
    private List<Node> m_exploredNodes;
    private List<Node> m_pathNodes;


    public Color startColor = Color.green;
    public Color goalColor = Color.red;
    public Color frontierColor = Color.magenta;
    public Color exploredColor = Color.gray;
    public Color pathColor = Color.cyan;

    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (graph == null || graphView == null || start == null || goal == null)
        {
            Debug.LogWarning("PATHFINDER Init error : missing Components");
            return;
        }

        if (start.nodeType == NodeType.Blocked || goal.nodeType == NodeType.Blocked)
        {
            Debug.LogWarning("PATHFINDER Init error : Start and Goal must be unblocked");
            return;
        }

        m_goalNode = goal;
        m_startNode = start;
        m_graph = graph;
        m_graphView = graphView;

        NodeView startNodeView = m_graphView.nodeViews[m_startNode.xIndex, m_startNode.yIndex];
        if (startNodeView != null)
        {
            startNodeView.ColorNode(startColor);
        }
        
        NodeView goalNodeView = m_graphView.nodeViews[m_goalNode.xIndex, m_goalNode.yIndex];
        if (goalNodeView != null)
        {
            goalNodeView.ColorNode(goalColor);
        }

        m_frontierNodes = new Queue<Node>();
        m_frontierNodes.Enqueue(m_startNode);

        m_exploredNodes = new List<Node>();
        m_pathNodes = new List<Node>();

        m_graph.ResetPreviousPathfinding();
    }

    

}
