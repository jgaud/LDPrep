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

    public int X { get; protected set; }
    public int Y { get; protected set; }

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

    public Tile(World world, int x, int y)
    {
        this.X = x;
        this.Y = y;
        this.world = world;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
