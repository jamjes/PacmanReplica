using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public enum Direction
    {
        TOP,
        LEFT,
        BOTTOM,
        RIGHT
    };
    
    public enum Type
    {
        PATH,
        WALL,
        PELLET,
        POWERPELLET
    };

    [SerializeField] private Color _baseColor, _altColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _coordinates;
    public int GCost {private set; get;} = 1;
    public int HCost {private set; get;}
    public int FCost {private set; get;}
    public Direction RelativeDirection {private set; get;}
    public Type NodeType; //{private set; get;}


    public Vector2 GetCoordinates()
    {
        return _coordinates;
    }

    public void SetType(Type newType)
    {
        NodeType = newType;
    }

    public void SetCoordinates(Vector2 position)
    {
        this._coordinates = position;
    }

    public void SetCosts(int hCost)
    {
        this.HCost = hCost;
        FCost = hCost + GCost;
    }

    public void SetPriority(Direction relativeDirection)
    {
        this.RelativeDirection = relativeDirection;
    }
}
