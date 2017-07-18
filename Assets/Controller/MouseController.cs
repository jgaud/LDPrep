using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject circleCursorPrefab;

    Vector3 lastFramePosition;
    Vector3 dragStartPosition;
    Vector3 currFramePosition;

    List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start () {
        dragPreviewGameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        //UpdateCursor();
        UpdateDragging();
        UpdateCameraMovement();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

    }

    //void UpdateCursor()
    //{
    //    Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
    //    if (tileUnderMouse != null)
    //    {
    //        circleCursor.SetActive(true);
    //        circleCursor.transform.position = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
    //    }
    //    else
    //    {
    //        circleCursor.SetActive(false);
    //    }
    //}

    void UpdateDragging()
    {
        //Start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }

        int startX = Mathf.FloorToInt(dragStartPosition.x);
        int endX = Mathf.FloorToInt(currFramePosition.x);
        if (endX < startX)
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

        //Cleanup old drag previews

        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (Input.GetMouseButton(0))
        {
            //Display a preview of the drag area
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        //Display the building hint on top of the tile position.
                        GameObject go = SimplePool.Spawn(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                    }
                }
            }
        }

        //End drag
        if (Input.GetMouseButtonUp(0))
        {
            

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }
                }
            }


        }
    }

    void UpdateCameraMovement()
    {
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector2 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
    }
}
