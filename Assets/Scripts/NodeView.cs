using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;
    public GameObject arrow;
    private Node m_node;

    [Range(0,0.5f)]
    public float borderSize = 0.15f;

    public void Init(Node node)
    {
        if (tile!= null)
        {
            gameObject.name = $"Node ({node.xIndex} , {node.yIndex})";
            gameObject.transform.position = node.position;

            m_node = node;
            EnableObject(arrow, false);

            // Setting border
            tile.transform.localScale = new Vector3(1f-borderSize, 1f,1f-borderSize);
        }
    }

    public void EnableObject(GameObject go, bool state)
    {
        if (go != null)
        {
            go.SetActive(state);
        }
    }

    public void ShowArrow( Color color)
    {
        if (m_node != null && arrow !=null && m_node.previous!= null)
        {
            EnableObject(arrow,true);
            Vector3 dirToPrevious = m_node.previous.position - m_node.position;
            arrow.transform.rotation = Quaternion.LookRotation(dirToPrevious.normalized);

            if (arrow.TryGetComponent<Renderer>(out Renderer rend))
            {
                rend.material.color = color;
            }
        }
    }

    public void ColorNode(Color color, GameObject go)
    {
        if (go.TryGetComponent<Renderer>(out Renderer rend))
        {
            rend.material.color = color;
        }
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }
}
