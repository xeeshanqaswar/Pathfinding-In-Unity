using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapData : MonoBehaviour
{
    public int width = 10;
    public int height = 5;

    public TextAsset mazeData;
    public Texture2D textureMap;
    public string ResourcePath = "Mapdata";

    private void Awake()
    {
        if (textureMap == null)
        {
            textureMap = Resources.Load(ResourcePath + "/TextureMaze") as Texture2D;
        }
        if (mazeData == null)
        {
            mazeData = Resources.Load(ResourcePath + "/DemoMaze") as TextAsset;
        }
    }

    public List<string> GetMapFromTextfile()
    {
        return GetMapFromTextfile(mazeData);
    }

    public List<string> GetMapFromTextfile(TextAsset tAsset)
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

    public List<string> GetMapfromTexture()
    {
        return GetMapfromTexture(textureMap);
    }

    public List<string> GetMapfromTexture(Texture2D texture)
    {
        List<string> lines = new List<string>();
        for (int y = 0; y < texture.height; y++)
        {
            string line = "";
            for (int x = 0; x < texture.width; x++)
            {
                if (texture.GetPixel(x, y) == Color.black)
                {
                    line += "1";
                }
                else if(texture.GetPixel(x, y) == Color.white)
                {
                    line += "0";
                }
                else
                {
                    line += " ";
                }
            }

            lines.Add(line);
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

        if (textureMap != null)
        {
            lines = GetMapfromTexture();
        }
        else
        {
            lines = GetMapFromTextfile();
        }

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
