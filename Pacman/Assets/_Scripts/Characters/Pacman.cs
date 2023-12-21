using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Vector2 Coordinates;
    private int pelletCount;
    private float _timeRef, _duration = 0.2f;
    [SerializeField] private PathGrid _gridRef;
    private Vector2? _targetPosition;
    public Vector2 _direction;
    private bool _isMoving;
    public bool run = false;
    public bool? win {private set; get;}

    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _spr;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Eat = Animator.StringToHash("Eat");

    private void Start()
    {
        _direction = Vector2.zero;
        win = null;
    }

    private void SetDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    private void Update()
    {
        var ghosts = FindObjectsOfType<GhostController>();
        foreach(GhostController g in ghosts)
        {
            if (Coordinates == g.CurrentPosition && g.CurrentState == Custom.Variables.GhostState.STUNNED)
            {
                Destroy(g.gameObject);
                break;
                
            }
            else if (Coordinates == g.CurrentPosition && g.CurrentState != Custom.Variables.GhostState.STUNNED)
            {
                if (FindObjectOfType<GameManager>()._init)
                {
                    foreach(GhostController h in ghosts)
                    {
                        h.Disable();
                    }
                    run = false;
                    if (win != false) win = false;
                    break;
                }
            }
        }
        

        SetDirection();
        
        if (_isMoving)
        {
            _timeRef += Time.deltaTime;
            float percentageComplete = _timeRef / _duration;

            if (_targetPosition != null) transform.position = Vector3.Lerp(Coordinates, (Vector2)_targetPosition, percentageComplete);
            
            if (percentageComplete >= 1)
            {
                Coordinates = transform.position;
                _timeRef = 0;
                _targetPosition = null;
                _isMoving = false;
                
                foreach(Pellet pellet in _gridRef.PelletPositions)
                {
                    if ((Vector2)pellet.transform.position == Coordinates)
                    {
                        if (pellet.GetComponent<SpriteRenderer>().sprite != null)
                        {
                            if (pellet._type == Custom.Variables.PelletType.POWER_PELLET)
                            {
                                FindObjectOfType<GameManager>().PowerPellet();
                            }
                            pellet.GetComponent<SpriteRenderer>().sprite = null;
                            pelletCount++;
                            CheckGameEnd();
                        }

                        break;
                    }
                    
                }
            }
        }
        else
        {
            if (run)
            {
                CheckMove();
            }
            
        }

        if (_isMoving)
        {
            _anim.CrossFade(Eat, 0,0);
        }
        else
        {
            _anim.CrossFade(Idle, 0,0);
        }

        switch(_direction)
        {
            case Vector2 v when v == Vector2.up:
                if (_spr.transform.rotation != Quaternion.Euler(0, 0, -90))
                {
                    _spr.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                
                if (!_spr.flipX)
                {
                    _spr.flipX = true;
                }

                break;
            
            case Vector2 v when v == Vector2.left:
                if (_spr.transform.rotation != Quaternion.Euler(0, 0, 0))
                {
                    _spr.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                
                if (!_spr.flipX)
                {
                    _spr.flipX = true;
                }
                break;
            
            case Vector2 v when v == Vector2.down:
                if (_spr.transform.rotation != Quaternion.Euler(0, 0, -90))
                {
                    _spr.transform.rotation = Quaternion.Euler(0, 0, -90);
                }

                if (_spr.flipX)
                {
                    _spr.flipX = false;
                }
                break;
            
            case Vector2 v when v == Vector2.right:
                if (_spr.transform.rotation != Quaternion.Euler(0, 0, 0))
                {
                    _spr.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                
                if (_spr.flipX)
                {
                    _spr.flipX = false;
                }
                break;
        }
    }

    private void CheckMove()
    {
        List<PathNode> neighbours = _gridRef.ReturnAllNeighbours(Coordinates);

        foreach(PathNode node in neighbours)
        {
            if (node.GetCoordinates() == Coordinates + _direction)
            {
                _targetPosition = Coordinates + _direction;
                _isMoving = true;
                break;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    public void SetPosition(Vector2 newPosition)
    {
        Coordinates = newPosition;
        transform.position = new Vector3(Coordinates.x, Coordinates.y, transform.position.z);
    }

    public void CheckGameEnd()
    {
        if (pelletCount == _gridRef.PelletPositions.Length)
        {
            if (win != true) win = true;
        }
    }


}
