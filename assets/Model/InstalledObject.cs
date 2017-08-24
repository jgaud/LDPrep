using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstalledObject
{

    public Tile tile { get; protected set; } //Represents only the base tile of the object

    public string objectType { get; protected set; }

    float movementCost; //If equal to 0, this tile is impassable
    int width;
    int height;

    Action<InstalledObject> cbOnChanged;

    //TODO: Implement rotation and larger objects 

    protected InstalledObject()
    {

    }

    static public InstalledObject CreatePrototype(string objectType, float movementCost = 1f, int width = 1, int height = 1)
    {
        InstalledObject obj = new InstalledObject();
        obj.objectType = objectType;
        obj.movementCost = movementCost;
        obj.width = width;
        obj.height = height;

        return obj;
    }

    static public InstalledObject PlaceInstance(InstalledObject proto, Tile tile)
    {

        InstalledObject obj = new InstalledObject();
        obj.objectType = proto.objectType;
        obj.movementCost = proto.movementCost;
        obj.width = proto.width;
        obj.height = proto.height;

        obj.tile = tile;

        if (tile.PlaceObject(obj) == false)
        {
            //Not able to place the object on the tile.
            return null;
        }


        return obj;
    }

    public void RegisterOnChangedCallback(Action<InstalledObject> callbackFunc)
    {
        cbOnChanged += callbackFunc;
    }

    public void UnregisterOnChangedCallback(Action<InstalledObject> callbackFunc)
    {
        cbOnChanged -= callbackFunc;
    }
}
