using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private PathNode _nodePrefab;
    [SerializeField] private Pellet _pelletPrefab;
    [SerializeField] private Transform _camera;
    public Pellet[] PelletPositions;
    private Dictionary<Vector2, PathNode> _nodes;
    private LayerMask _wallLayer;
    private Vector2[] wallPositions = 
    {
        new Vector2(5,1), new Vector2(14,1),
        new Vector2(2,2), new Vector2(3,2), new Vector2(5,2), new Vector2(7,2), new Vector2(8,2), new Vector2(9,2), new Vector2(10,2), new Vector2(11,2), new Vector2(12,2), new Vector2(14,2), new Vector2(16,2), new Vector2(17,2), 
        new Vector2(2,3), new Vector2(17,3),
        new Vector2(2,4), new Vector2(4,4), new Vector2(5,4), new Vector2(7,4), new Vector2(8,4), new Vector2(9,4), new Vector2(10,4), new Vector2(11,4), new Vector2(12,4), new Vector2(14,4), new Vector2(15,4), new Vector2(17,4), 
        new Vector2(7,5), new Vector2(12,5), 
        new Vector2(2,6), new Vector2(4,6), new Vector2(5,6), new Vector2(7,6), new Vector2(8,6), new Vector2(9,6), new Vector2(10,6), new Vector2(11,6), new Vector2(12,6), new Vector2(14,6), new Vector2(15,6), new Vector2(17,6), 
        new Vector2(2,7), new Vector2(17,7), 
        new Vector2(2,8), new Vector2(3,8), new Vector2(5,8), new Vector2(7,8), new Vector2(8,8), new Vector2(9,8), new Vector2(10,8), new Vector2(11,8), new Vector2(12,8), new Vector2(14,8), new Vector2(16,8), new Vector2(17,8), 
        new Vector2(5,9), new Vector2(14,9)
        
    };

    private void Start()
    {
        _wallLayer = LayerMask.NameToLayer("Wall");
        GenerateGrid();
        PelletPositions = new Pellet[94];
        
        var allPellets = FindObjectsOfType<Pellet>();
        
        for(int count = 0; count < allPellets.Length; count++)
        {
            PelletPositions[count] = allPellets[count];
        }
    }
    
    public PathNode GetNodeAtPosition(Vector2 position)
    {
        if (_nodes.TryGetValue(position, out var pathNode))
        {
            return pathNode;
        }
        
        return null;
    }
    
    private void SetWalls()
    {
        foreach(PathNode node in _nodes.Values)
        {
            if ((node.GetCoordinates().x == 0 || node.GetCoordinates().x == _width - 1) || ((node.GetCoordinates().y == 0 || node.GetCoordinates().y == _height - 1)))
            {
                GetNodeAtPosition(node.GetCoordinates()).gameObject.layer = _wallLayer.value;
            }
        }

        foreach(Vector2 wall in wallPositions)
        {
            GetNodeAtPosition(wall).gameObject.layer = _wallLayer.value;
        }
    }
    
    private void GenerateGrid()
    {
        _nodes = new Dictionary<Vector2, PathNode>();
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Vector2 position = new Vector2(x,y);
                var newNode = Instantiate(_nodePrefab, position, Quaternion.identity);
                newNode.name = $"Path Node [{x},{y}]";
                newNode.SetCoordinates(position);
                _nodes[position] = newNode;
            }
        }
        
        SetWalls();

    }

    public List<PathNode> ReturnAllNeighbours(Vector2 currentPosition)
    {
        List<PathNode> neighbours = new List<PathNode>();
        PathNode top = GetNodeAtPosition(new Vector2(currentPosition.x, currentPosition.y + 1));
        if (top.gameObject.layer != _wallLayer.value) neighbours.Add(top);

        PathNode left = GetNodeAtPosition(new Vector2(currentPosition.x - 1, currentPosition.y));
        if (left.gameObject.layer != _wallLayer.value) neighbours.Add(left);

        PathNode bottom = GetNodeAtPosition(new Vector2(currentPosition.x, currentPosition.y - 1));
        if (bottom.gameObject.layer != _wallLayer.value) neighbours.Add(bottom);

        PathNode right = GetNodeAtPosition(new Vector2(currentPosition.x + 1, currentPosition.y));
        if (right.gameObject.layer != _wallLayer.value) neighbours.Add(right);

        return neighbours;
    }

    private List<PathNode> ReturnNeighbours(Vector2 currentPosition, Vector2 previousPosition)
    {
        List<PathNode> neighbours = new List<PathNode>();
        PathNode top = GetNodeAtPosition(new Vector2(currentPosition.x, currentPosition.y + 1));
        PathNode left = GetNodeAtPosition(new Vector2(currentPosition.x - 1, currentPosition.y));
        PathNode bottom = GetNodeAtPosition(new Vector2(currentPosition.x, currentPosition.y - 1));
        PathNode right = GetNodeAtPosition(new Vector2(currentPosition.x + 1, currentPosition.y));
        
        if (top != null)
        {
            if ((top.gameObject.layer != _wallLayer.value) && (top.GetCoordinates() != previousPosition))
            {
                top.SetPriority(PathNode.Direction.TOP);
                neighbours.Add(top);
            }
        }

        if (left != null)
        {
            if ((left.gameObject.layer != _wallLayer.value)&& (left.GetCoordinates() != previousPosition))
            {
                left.SetPriority(PathNode.Direction.LEFT);
                neighbours.Add(left);
            }
        }

        if (bottom != null)
        {
            if ((bottom.gameObject.layer != _wallLayer.value) && (bottom.GetCoordinates() != previousPosition))
            {
                bottom.SetPriority(PathNode.Direction.BOTTOM);
                neighbours.Add(bottom);
            }
        }

        if (right != null)
        {
            if ((right.gameObject.layer != _wallLayer.value) && (right.GetCoordinates() != previousPosition))
            {
                right.SetPriority(PathNode.Direction.RIGHT);
                neighbours.Add(right);
            }
        }
        
        return neighbours;
    }

    public PathNode ShortestPathNode(Vector2 previousPosition, Vector2 startPosition, Vector2 targetPosition)
    {
        List<PathNode> neighbours = ReturnNeighbours(startPosition, previousPosition);
        
        if (neighbours.Count > 1)
        {
            foreach(PathNode node in neighbours)
            {
                node.SetCosts(CalculateHCost(node.GetCoordinates(), targetPosition));
                //Debug.Log(node.gameObject.name + " has an fCost of " + node.FCost);
            }

            PathNode temp;

            for(int iteration = 0; iteration < neighbours.Count; iteration ++)
            {
                for (int targetPos = 0; targetPos < neighbours.Count - 1; targetPos++)
                {
                    if (neighbours[targetPos].FCost > neighbours[targetPos + 1].FCost)
                    {
                        temp = neighbours[targetPos + 1];
                        neighbours[targetPos + 1] = neighbours[targetPos];
                        neighbours[targetPos] = temp;
                    }
                    else if (neighbours[targetPos].FCost == neighbours[targetPos + 1].FCost)
                    {
                        if (neighbours[targetPos].RelativeDirection > neighbours[targetPos + 1].RelativeDirection)
                        {
                            temp = neighbours[targetPos + 1];
                            neighbours[targetPos + 1] = neighbours[targetPos];
                            neighbours[targetPos] = temp;
                        }
                    }
                }
            }
        }

        return neighbours[0];
    }

    private int CalculateHCost(Vector2 startPosition, Vector2 targetPosition)
    {
        int tempX = (int)targetPosition.x - (int)startPosition.x;
        if (tempX < 0) tempX *= -1;
        
        int tempY = (int)targetPosition.y - (int)startPosition.y;
        if (tempY < 0) tempY *= -1;

        return tempX + tempY;
    }
}
