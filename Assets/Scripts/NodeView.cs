using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;

    [Range(0,0.5f)]
    public float borderSize = 0.15f;

    public void Init(Node node)
    {
        if (tile!= null)
        {
            gameObject.name = $"Node ({node.xIndex} , {node.yIndex})";
            gameObject.transform.position = node.position;

            // Setting border
            tile.transform.localScale = new Vector3(1f-borderSize, 1f-borderSize);
        }
    }

    private void ColorNode(Color color, GameObject go)
    {
        if (go.TryGetComponent<Renderer>(out Renderer rend))
        {
            rend.material.color = color;
        }
    }

    private void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }
}
