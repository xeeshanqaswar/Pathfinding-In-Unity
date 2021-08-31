﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapData : MonoBehaviour
{
    public int width = 10;
    public int height = 5;
    public TextAsset mazeData;

    public List<string> GetTextFromFile()
    {
        return GetTextFromFile(mazeData);
    }

    public List<string> GetTextFromFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();

        if (tAsset != null)
        {
            string textData = tAsset.text;
            string[] delimeters = { "\r\n", "\n" };
            lines.AddRange(textData.Split(delimeters,StringSplitOptions.None));
            lines.Reverse();
        }
        else
        {
            Debug.Log("MAPDATA GetTextFromFile Error: invalid TextAsset");
        }

        return lines;
    }

    public void SetDimensions(List<string> textLines)
    {
        height = textLines.Count;
        foreach (string line in textLines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }
    }

    public int[,] MakeMap()
    {
        List<string> lines = new List<string>();
        lines = GetTextFromFile();
        SetDimensions(lines);

        int[,] map = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (lines[y].Length > x)
                {
                    map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }


        return map;
    }

}
