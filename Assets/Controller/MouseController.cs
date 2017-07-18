using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	Vector3 lastFramePosition;
    Vector3 dragStartPosition;
    public GameObject circleCursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        //Updating the circle cursor position

        Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
        if (tileUnderMouse != null)
        {
            circleCursor.SetActive(true);
            circleCursor.transform.position = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
        }
        else
        {
            circleCursor.SetActive(false);
        }

        //Handle the left mouse clicks

        //Start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }

        //End drag
        if (Input.GetMouseButtonUp(0))
        {
            int startX = Mathf.FloorToInt(dragStartPosition.x);
            int endX = Mathf.FloorToInt(currFramePosition.x);
            if(endX < startX)
            {
                int tmp = endX;
                endX = startX;
                startX = tmp;
            }

            int startY = Mathf.FloorToInt(dragStartPosition.y);
            int endY = Mathf.FloorToInt(currFramePosition.y);
            if (endY < startY)
            {
                int tmp = endY;
                endY = startY;
                startY = tmp;
            }

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if(t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }
                }
            }

            
        }

        //Screen dragging
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector2 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
	}

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return WorldController.Instance.World.GetTileAt(x, y);
    }
}
