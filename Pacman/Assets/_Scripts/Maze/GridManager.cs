using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour, IGridInterface
{
    private int height, width;
    public Tile TilePrefab;

    public Tile[,] Nodes;

    private Color dullBlue;

    private Vector2[] wallPositions;
    public Transform cam;

    private void Start()
    {
        float r = 0.3568628f;
        float g = 0.4313726f;
        float b = 0.8823529f;
        dullBlue = new Color(r,g,b, 1);
        height = 8;
        width = 8;
        InitWallPositions();
        Nodes = GenerateGrid();
    }

    private void InitWallPositions()
    {
        wallPositions = new Vector2[24];
        wallPositions[0] = new Vector2(1,1);        
        wallPositions[1] = new Vector2(2,1);
        wallPositions[2] = new Vector2(3,1);
        wallPositions[3] = new Vector2(4,1);
        wallPositions[4] = new Vector2(5,1);
        wallPositions[5] = new Vector2(6,1);

        wallPositions[6] = new Vector2(4,2);

        wallPositions[7] = new Vector2(0,3);
        wallPositions[8] = new Vector2(2,3);
        wallPositions[9] = new Vector2(4,3);
        wallPositions[10] = new Vector2(6,3);
        wallPositions[11] = new Vector2(7,3);

        wallPositions[12] = new Vector2(2,4);

        wallPositions[13] = new Vector2(1,5);
        wallPositions[14] = new Vector2(2,5);
        wallPositions[15] = new Vector2(4,5);
        wallPositions[16] = new Vector2(5,5);
        wallPositions[17] = new Vector2(6,5);

        wallPositions[18] = new Vector2(0,7);
        wallPositions[19] = new Vector2(1,7);
        wallPositions[20] = new Vector2(2,7);
        wallPositions[21] = new Vector2(4,7);
        wallPositions[22] = new Vector2(6,7);
        wallPositions[23] = new Vector2(7,7);        
    }

    private Tile[,] GenerateGrid()
    {
        Tile [,] nodeArray = new Tile[8,8];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(TilePrefab, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity);
                spawnedTile.name = $"Tile ({x},{y})";
                spawnedTile.SetCoordinates(x,y);
                if (CheckWallTile(spawnedTile.Coordinates))
                {
                    spawnedTile.gameObject.layer = LayerMask.NameToLayer("Wall");
                }
                nodeArray[x,y] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)width/2-0.5f, (float)height/2-0.5f, -10);

        return nodeArray;
    }

    private bool CheckWallTile(Vector2 targetPosition)
    {
        foreach(Vector2 position in wallPositions)
        {
            if (position == targetPosition)
            {
                return true;
            }
        }

        return false;
        
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return null;
        }
        else
        {
            return Nodes[x, y];
        }
    }
}
