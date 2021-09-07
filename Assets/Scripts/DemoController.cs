using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initialize Mapdata and feed it to Graph.
// Initialize GraphView
// Define Goal Node and Start Node.


public class DemoController : MonoBehaviour
{

    public MapData mapData;
    public Graph graph;

    public Pathfinder pathFinder;
    public Vector2Int startNode;
    public Vector2Int goalNode;

    private void Start()
    {

        if (mapData != null && graph != null)
        {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);

            GraphView graphView = graph.GetComponent<GraphView>();

            if (graphView != null)
            {
                graphView.Init(graph);
            }
            bool first = graph.IsWithinBounds(startNode.x, startNode.y);
            bool second = graph.IsWithinBounds(goalNode.x, goalNode.y);
            if (first && second && pathFinder != null)
            {
                pathFinder.Init(graph, graphView, graph.nodes[startNode.x, startNode.y], graph.nodes[goalNode.x, goalNode.y]);
            }

        }


    }

}
