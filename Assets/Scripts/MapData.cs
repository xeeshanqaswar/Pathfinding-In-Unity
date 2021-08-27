using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    public int width = 10;
    public int height = 5;

    public int[,] MakeMap()
    {
        int[,] map = new int[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = 0;
            }
        }

        return map;
    }

}
