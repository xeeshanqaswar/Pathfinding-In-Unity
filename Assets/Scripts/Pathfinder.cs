using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// BFS TREE : Explore each node before reaching goal. 


public enum Mode
{
    BreadthFirstSearch, Dijkstra, GreedyBestFirst
}

public class Pathfinder : MonoBehaviour
{
    public Mode mode = Mode.BreadthFirstSearch;

    public Color startColor = Color.green;
    public Color goalColor = Color.red;
    public Color frontierColor = Color.magenta;
    public Color exploredColor = Color.gray;
    public Color pathColor = Color.cyan;
    public Color arrowColor = Color.cyan;
    public Color highLightColor = Color.cyan;

    [Header("FLAGS")]
    public bool showIterations;
    public bool showColor;
    public bool showArrow;
    public bool exitOnGoal;

    public bool isComplete = false;
    private int m_iterations = 0;

    private Node m_startNode;
    private Node m_goalNode;
    private Graph m_graph;
    private GraphView m_graphView;

    private PriorityQueue<Node> m_frontierNodes;
    private List<Node> m_exploredNodes;
    private List<Node> m_pathNodes;

    
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

        ColorNodes();

        m_frontierNodes = new PriorityQueue<Node>();
        m_frontierNodes.Enqueue(m_startNode);

        m_exploredNodes = new List<Node>();
        m_pathNodes = new List<Node>();

        // Reset Pathfinding....
        m_graph.ResetPreviousPathfinding();
        isComplete = false;
        m_iterations = 0;
        m_startNode.distanceTravelled = 0;
    }

    private void ColorNodes(bool lerpColor = false, float lerpValue = 0.5f)
    {
        if (m_frontierNodes != null)
        {
            m_graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor, lerpColor, lerpValue);
        }

        if (m_exploredNodes != null)
        {
            m_graphView.ColorNodes(m_exploredNodes, exploredColor, lerpColor, lerpValue);
        }

        if (m_pathNodes != null && m_pathNodes.Count > 0)
        {
            m_graphView.ColorNodes(m_pathNodes, pathColor, lerpColor, lerpValue * 2f);
        }

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
    }

    public IEnumerator SearchRoutine(float timeStamp = 0.1f)
    {
        yield return null;
        float timeStart = Time.time;

        while (!isComplete)
        {
            if (m_frontierNodes.Count > 0)
            {
                m_iterations++;
                Node currentNode = m_frontierNodes.Dequeue();

                if (!m_exploredNodes.Contains(currentNode))
                {
                    m_exploredNodes.Add(currentNode);
                }

                #region DECIDE WHICH ALGORITHM RUNS
                if (mode == Mode.BreadthFirstSearch)
                {
                    ExpandFrontierBreadthFirst(currentNode);
                }
                else if (mode == Mode.Dijkstra)
                {
                    ExpandFrontierDijkstra(currentNode);
                }
                else if (mode == Mode.GreedyBestFirst)
                {
                    ExpandFrontierGreedyBreadthFirst(currentNode);
                }

                #endregion

                if (m_frontierNodes.Contains(m_goalNode))
                {
                    m_pathNodes = GetPathNodes(m_goalNode); 
                    
                    if (exitOnGoal)
                    {
                        isComplete = true;
                        Debug.Log("PATHFINDER mode : " + mode.ToString() + " distane travelled : " + m_goalNode.distanceTravelled );
                    }
                }

                if (showIterations)
                {
                    ShowDiagnostics(true, 0.5f);
                    yield return new WaitForSeconds(timeStamp);
                }
            }
            else
            {
                isComplete = true;
            }
        }

        ShowDiagnostics(true, 0.5f); // If you only want colors and Arrows but not in each interation.
        Debug.Log("PATHFINDER SearchRoutine Elapsed Time : " + (Time.time - timeStart).ToString() + " seconds!");
    }

    private void ShowDiagnostics(bool lerpColor = false, float lerpValue = 0.5f)
    {
        if (showArrow)
        {
            m_graphView.ShowNodeArrow(m_frontierNodes.ToList(), arrowColor);
        }

        if (m_frontierNodes.Contains(m_goalNode) && showArrow)
        {
            m_graphView.ShowNodeArrow(m_pathNodes, highLightColor);
        }

        if (showColor)
        {
            ColorNodes(lerpColor,lerpValue);
        }
    }

    private void ExpandFrontierBreadthFirst(Node node)
    {
        for (int i = 0; i < node.neighbours.Count; i++)
        {
            if (!m_frontierNodes.Contains(node.neighbours[i]) &&
                !m_exploredNodes.Contains(node.neighbours[i]))
            {
                float distanceToNeighbour = m_graph.GetNodeDistance(node, node.neighbours[i]);
                float newDistanceTravelled = distanceToNeighbour + node.distanceTravelled + (int)node.neighbours[i].nodeType;
                node.neighbours[i].distanceTravelled = newDistanceTravelled;

                node.neighbours[i].previous = node;
                node.neighbours[i].priority = m_exploredNodes.Count; // Trick to use Priority Queue with BFS

                m_frontierNodes.Enqueue(node.neighbours[i]);
            }
        }
    }

    private void ExpandFrontierGreedyBreadthFirst(Node node)
    {
        for (int i = 0; i < node.neighbours.Count; i++)
        {
            if (!m_frontierNodes.Contains(node.neighbours[i]) &&
                !m_exploredNodes.Contains(node.neighbours[i]))
            {
                float distanceToNeighbour = m_graph.GetNodeDistance(node, node.neighbours[i]);
                float newDistanceTravelled = distanceToNeighbour + node.distanceTravelled + (int)node.neighbours[i].nodeType;
                node.neighbours[i].distanceTravelled = newDistanceTravelled;

                node.neighbours[i].previous = node;
                node.neighbours[i].priority = (int)m_graph.GetNodeDistance(node.neighbours[i],m_goalNode);

                m_frontierNodes.Enqueue(node.neighbours[i]);
            }
        }
    }

    private void ExpandFrontierDijkstra(Node node)
    {
        for (int i = 0; i < node.neighbours.Count; i++)
        {
            if (!m_exploredNodes.Contains(node.neighbours[i]))
            {
                float distanceToNeighbour = m_graph.GetNodeDistance(node, node.neighbours[i]);
                float newDistanceTravelled = distanceToNeighbour + node.distanceTravelled + (int)node.neighbours[i].nodeType;

                // if Neighbour node distance is Infinity i.e. initial distance or
                // newDistance is smaller than previous distance.
                if (float.IsPositiveInfinity(node.neighbours[i].distanceTravelled) ||
                    newDistanceTravelled < node.neighbours[i].distanceTravelled)
                {
                    node.neighbours[i].previous = node;
                    node.neighbours[i].distanceTravelled = newDistanceTravelled;
                }

                if (!m_frontierNodes.Contains(node.neighbours[i]))
                {
                    node.neighbours[i].priority = (int)node.neighbours[i].distanceTravelled;
                    m_frontierNodes.Enqueue(node.neighbours[i]);
                }
            }
        }
    }

    private List<Node> GetPathNodes(Node endNode)
    {
        List<Node> path = new List<Node>();

        if (endNode == null)
        {
            return path;
        }

        path.Add(endNode);

        Node currentNode = endNode.previous;

        while(currentNode != null)
        {
            path.Insert(0, currentNode); // Inserting elements at start and pushing other elements down
            currentNode = currentNode.previous;
        }

        return path;
    }

}
