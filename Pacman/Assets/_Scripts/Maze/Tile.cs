using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 Coordinates;

    public void SetCoordinates(int x, int y)
    {
        Coordinates = new Vector2(x,y);
    }
}
