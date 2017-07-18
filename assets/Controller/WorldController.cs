using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    static WorldController _instance;
    public static WorldController Instance { get; protected set; }

    public Sprite floorSprite;

    public World World;
	// Use this for initialization
	void Start () {
        if(_instance != null)
        {
            Debug.LogError("There shouldn't be more than one world controller.");
        }
        Instance = _instance = this;

        World = new World();

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);
                GameObject tile_go = new GameObject()
                {
                    name = "Tile_" + x + "_" + y
                };
                tile_go.transform.position = new Vector2(tile_data.X, tile_data.Y);

				tile_go.transform.SetParent (this.transform, true);

                tile_go.AddComponent<SpriteRenderer>();

                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }
        World.RandomizeTiles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if(tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if(tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("Unrecognized tile type.");
        }
    }

    
}
