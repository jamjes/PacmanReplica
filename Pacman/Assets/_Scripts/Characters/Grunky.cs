using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;

public class Grunky : GhostController
{
    public PathGrid Pathfinding;
    public Pacman Pacman;
    private bool _move;
    public float _duration = 0.25f;
    [SerializeField] private bool _targetPacman, _angry, _scared;
    private PathNode _nextPosition;
    private bool _found;
    [SerializeField] private Vector2 _homePosition;

    private void Update()
    {
        if (!_move && run)
        {
            switch(CurrentState)
            {
                case GhostState.IDLE:

                    break;

                case GhostState.SCATTER:
                    if (_duration != 0.25f) _duration = 0.25f;
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, _homePosition);
                    _move = true;
                    break;

                case GhostState.CHASE:
                    if (_duration != 0.25f) _duration = 0.25f;
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, Pacman.Coordinates);
                    _move = true;
                    break;

                case GhostState.STUNNED:
                    while (!_found)
                    {
                        if (_duration != 0.3f) _duration = 0.3f;
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
                case GhostState.ANGRY_CHASE:
                    if (_duration != 0.2f) _duration = 0.2f;
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, Pacman.Coordinates);
                    _move = true;
                    break;
                case GhostState.SCARED_CHASE:
                    if (_duration != 0.25f) _duration = 0.25f;
                    _nextPosition = Pathfinding.ShortestPathNode(PreviousPosition, CurrentPosition, _homePosition);
                    _move = true;
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

    public void CalculateStun()
    {
        var ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        switch(ghosts.Length)
        {
            case 1:
                SetState(GhostState.SCARED_CHASE);
                _anim.SetDynamicSpriteSheet("SCARED");
            break;

            case 2:
                SetState(GhostState.ANGRY_CHASE);
                _anim.SetDynamicSpriteSheet("ANGRY");
            break;
            
            case 3:
                SetState(GhostState.CHASE);
                _anim.SetDynamicSpriteSheet("STANDARD");
            break;
        }
    }
}
