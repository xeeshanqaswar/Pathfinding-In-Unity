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

    public float TimeStamp;

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

            if (graph.IsWithinBounds(startNode.x, startNode.y) && 
                graph.IsWithinBounds(goalNode.x, goalNode.y) && 
                pathFinder != null)
            {
                pathFinder.Init(graph, graphView, graph.nodes[startNode.x, startNode.y], graph.nodes[goalNode.x, goalNode.y]);
                StartCoroutine(pathFinder.SearchRoutine(TimeStamp));
            }

        }


    }

}
