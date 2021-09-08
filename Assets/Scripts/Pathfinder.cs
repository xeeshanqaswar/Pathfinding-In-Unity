﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    public Color arrowColor = Color.cyan;
    public Color highLightColor = Color.cyan;

    public bool isComplete = false;
    private int m_iterations = 0;

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

        m_frontierNodes = new Queue<Node>();
        m_frontierNodes.Enqueue(m_startNode);

        m_exploredNodes = new List<Node>();
        m_pathNodes = new List<Node>();

        // Reset Pathfinding....
        m_graph.ResetPreviousPathfinding();
        isComplete = false;
        m_iterations = 0;
    }

    private void ColorNodes()
    {
        if (m_frontierNodes != null)
        {
            m_graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor);
        }

        if (m_exploredNodes != null)
        {
            m_graphView.ColorNodes(m_exploredNodes, exploredColor);
        }

        if (m_pathNodes != null && m_pathNodes.Count > 0)
        {
            m_graphView.ColorNodes(m_pathNodes, pathColor);
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
        while (!isComplete)
        {
            if (m_frontierNodes.Count > 0)
            {
                Node currentNode = m_frontierNodes.Dequeue();
                m_iterations++;

                if (!m_exploredNodes.Contains(currentNode))
                {
                    m_exploredNodes.Add(currentNode);
                }

                ExpandFrontier(currentNode);

                ColorNodes();
                m_graphView.ShowNodeArrow(m_frontierNodes.ToList(), arrowColor);

                if (m_frontierNodes.Contains(m_goalNode))
                {
                    m_pathNodes = GetPathNodes(m_goalNode);
                    m_graphView.ShowNodeArrow(m_pathNodes, highLightColor);
                }
                
                yield return new WaitForSeconds(timeStamp);
            }
            else
            {
                isComplete = true;
            }
        }
    }

    private void ExpandFrontier(Node node)
    {
        for (int i = 0; i < node.neighbours.Count; i++)
        {
            if (!m_frontierNodes.Contains(node.neighbours[i]) &&
                !m_exploredNodes.Contains(node.neighbours[i]))
            {
                node.neighbours[i].previous = node;
                m_frontierNodes.Enqueue(node.neighbours[i]);
            }
        }
    }

    List<Node> GetPathNodes(Node endNode)
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
            path.Insert(0, currentNode);
            currentNode = currentNode.previous;
        }

        return path;
    }

}
