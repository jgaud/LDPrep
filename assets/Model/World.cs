using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World  {

    Tile[,] tiles;

    Dictionary<string, InstalledObject> installedObjectPrototypes;

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    Action<InstalledObject> cbInstalledObjectCreated;

    public World(int width = 100, int height = 100)
    {
        this.Width = width;
        this.Height = height;

        

        tiles = new Tile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = new Tile(this, i, j);
            }
        }

        CreateInstalledObjectProtoypes();

        

    }

    protected void CreateInstalledObjectProtoypes()
    {
        installedObjectPrototypes = new Dictionary<string, InstalledObject>();

        InstalledObject wallPrototype = InstalledObject.CreatePrototype("Wall", 0);

        installedObjectPrototypes.Add("Wall", wallPrototype);
    }

    public Tile GetTileAt(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height)
        {
            Debug.Log("Tile : (" + x + "," + y + ")");
            return null;
        }
        else
        {
            return tiles[x, y];
        }
        
    }

    public void PlaceInstalledObject(string objectType, Tile tile)
    {
        //Assume 1x1 tile
        if(installedObjectPrototypes.ContainsKey(objectType) == false)
        {
            Debug.LogError("Object: " + objectType + "doesn't exist");
            return;
        }

        InstalledObject obj = InstalledObject.PlaceInstance(installedObjectPrototypes[objectType], tile);

        if(obj == null)
        {
            //Failed to place object, something there already?
            return;
        }

        if(cbInstalledObjectCreated != null)
        {
            cbInstalledObjectCreated(obj);
        }
    }

    public void RegisterInstalledObjectCreated(Action<InstalledObject> callbackFunc)
    {
        cbInstalledObjectCreated += callbackFunc;
    }

    public void UnregisterInstalledObjectCreated(Action<InstalledObject> callbackFunc)
    {
        cbInstalledObjectCreated -= callbackFunc;
    }

    public void RandomizeTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    tiles[x, y].Type = TileType.Empty;
                }
                else
                {
                    tiles[x, y].Type = TileType.Floor;
                }
            }
        }
    }


}
