using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World  {

    Tile[,] tiles;
    public int Width { get; protected set; }
    public int Height { get; protected set; }

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

    }



    public Tile GetTileAt(int x, int y)
    {
        if(x < 0 || x > Width || y < 0 || y > Height)
        {
            Debug.Log("Tile : (" + x + "," + y + ")");
            return null;
        }
        return tiles[x, y];
    }

    public void RandomizeTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    tiles[x, y].Type = Tile.TileType.Empty;
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Floor;
                }
            }
        }
    }


}
