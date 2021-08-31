using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{

    public MapData mapData;
    public Graph graph;

    private void Start()
    {

        if (mapData != null && graph != null)
        {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);

            if (graph.TryGetComponent<GraphView>(out GraphView graphView))
            {
                graphView.Init(graph);
            }

        }

    }

}
