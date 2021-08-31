using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;
    public Color baseColor = Color.white;
    public Color wallColor = Color.black;

    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.Log("GRAPHVIEW : graph is not defined");
            return;
        }

        foreach (Node n in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);

            if (instance.TryGetComponent<NodeView>(out NodeView nodeView))
            {
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

}
