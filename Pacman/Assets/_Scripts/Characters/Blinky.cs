using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;


public class Blinky : GhostController
{
    public PathGrid Pathfinding;
    public Pacman Pacman;
    private bool _move, _found;
    public float _duration = 0.25f;
    [SerializeField] private bool _targetPacman, _angry;
    [SerializeField] private Vector2 _homePosition = new Vector2(19,7); 
    private PathNode _nextPosition;

    private void Update()
    {
        if (!_move && run)
        {
            switch(CurrentState)
            {
                case GhostState.IDLE:
                    break;

                case GhostState.SCATTER:
                    
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, _homePosition);
                    _move = true;
                    break;

                case GhostState.CHASE:
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, Pacman.Coordinates);
                    _move = true;
                    break;

                case GhostState.STUNNED:
                    while (!_found)
                    {
                        int randomX = Random.Range(0,20);
                        int randomY = Random.Range(0,11);
                        Vector2 randomPosition = new Vector2(randomX, randomY);
                        if (Pathfinding.GetNodeAtPosition(randomPosition).gameObject.layer != LayerMask.NameToLayer("Wall"))
                        {
                            _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, randomPosition);
                            _found = true;
                            _move = true;
                        }
                    }
                    break;
                }
            }
            else if (_move)
            {
                SetPosition(_nextPosition.GetCoordinates(), "T");
                SetCurrentDirection();
                _move = MoveToTargetTile(_duration);
            }
        }
    }