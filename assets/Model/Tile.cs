using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

    public enum TileType { Empty, Floor};
    TileType type = TileType.Empty;

    Action<Tile> cbTileTypeChanged;

    LooseObject looseObject;
    InstalledObject installedObject;

    World world;

    int x;
    int y;

    public TileType Type
    {
        get
        {
            return type;
        }

        set
        {
            TileType oldType = type;
            type = value;
            //Call the callback to update the view.
            if(cbTileTypeChanged != null && type != oldType)
            {
                cbTileTypeChanged(this);
            }
        }
    }

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public Tile(World world, int x, int y)
    {
        this.X = x;
        this.y = y;
        this.world = world;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void unregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
