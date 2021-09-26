using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Linq;

// Result of this Script is 2d Array with 0s and 1s which define Map 
// 0s define open Nodes while 1s define walls
// Used this data in Graph.

public class MapData : MonoBehaviour
{
    public int width = 10;
    public int height = 5;

    public TextAsset mazeData;
    public Texture2D textureMap;
    public string ResourcePath = "Mapdata";

    public Color32 openColor = Color.white;
    public Color32 blockedColor = Color.black;
    public Color32 lightTerrainColor = new Color32(124,194,78,255);
    public Color32 mediumTerrainColor = new Color32(252, 255, 52, 255);
    public Color32 heavyTerrainColor = new Color32(255, 129, 12, 255);

    static Dictionary<Color32, NodeType> terrainLookupTable = new Dictionary<Color32, NodeType>();

    private void Awake()
    {
        SetUpLookupTable();

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
                Color pixelColor = texture.GetPixel(x,y);
                if (terrainLookupTable.ContainsKey(pixelColor))
                {
                    NodeType nodeType = terrainLookupTable[pixelColor];
                    int nodeTypeNum = (int)nodeType;
                    line += nodeTypeNum; 
                }
                else
                {
                    line += '0';
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

    private void SetUpLookupTable()
    {
        terrainLookupTable.Add(openColor, NodeType.Open);
        terrainLookupTable.Add(blockedColor, NodeType.Blocked);
        terrainLookupTable.Add(lightTerrainColor, NodeType.LightTerrain);
        terrainLookupTable.Add(mediumTerrainColor, NodeType.MediumTerrain);
        terrainLookupTable.Add(heavyTerrainColor, NodeType.HeavyTerrain);
    }

    public static Color GetColorFromNodeType(NodeType type)
    {
        if (terrainLookupTable.ContainsValue(type))
        {
            return terrainLookupTable.FirstOrDefault(x => x.Value == type).Key;
        }
        return Color.white;
    }
}
