using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;

public class GameManager : MonoBehaviour
{
    public bool _init;
    [SerializeField] private Pacman _pacmanRef;
    [SerializeField] private Blinky _blinkyRef;
    [SerializeField] private Pinky _pinkyRef;
    [SerializeField] private Grunky _grunkyRef;
    [SerializeField] private PathGrid _gridRef;
    private bool frightened = false;
    private bool _start;

    [SerializeField] private Vector2 _pacmanSpawnPosition, _blinkySpawnPosition, _pinkySpawnPosition, _grunkySpawnPosition;

    private float timerRef = 3;
    private bool countDown = true;

    private void Update()
    {
        if (!_init)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Init();
            }
        }
        else
        {
            if(!_start)
        {
            timerRef -= Time.deltaTime;

            if (timerRef < 0)
            {
                _blinkyRef.SetState(GhostState.SCATTER);
                _pinkyRef.SetState(GhostState.SCATTER);
                _grunkyRef.SetState(GhostState.SCATTER);
                _start = true;
                timerRef = 3;
            }
        }
        else
        {
            if (frightened)
            {
                timerRef -= Time.deltaTime;

                if (timerRef < 0)
                {
                    frightened = false;

                    if (_blinkyRef != null)
                    {
                        _blinkyRef.SetState(GhostState.CHASE);
                        _blinkyRef._anim.SetDynamicSpriteSheet("STANDARD");
                        _blinkyRef._duration = 0.25f;
                    }

                    if (_pinkyRef != null)
                    {
                        _pinkyRef.SetState(GhostState.CHASE);
                        _pinkyRef._anim.SetDynamicSpriteSheet("STANDARD");
                        _pinkyRef._duration = 0.25f;
                    }
                    
                    if (_grunkyRef != null)
                    {
                        _grunkyRef.SetState(GhostState.CHASE);
                        _grunkyRef.CalculateStun();
                        _grunkyRef._duration = 0.25f;
                    }
                    
                    timerRef = 20;
                }
            }
            else
            {
                timerRef -= Time.deltaTime;
            
                if (timerRef < 0)
                {
                    if (_blinkyRef != null && _blinkyRef.CurrentState == GhostState.SCATTER)
                    {
                        _blinkyRef.SetState(GhostState.CHASE);
                        if (timerRef < 0) timerRef = 20;
                    }
                    else
                    {
                        _blinkyRef.SetState(GhostState.SCATTER);
                        if (timerRef < 0) timerRef = 3;
                    }

                    if (_pinkyRef != null && _pinkyRef.CurrentState == GhostState.SCATTER)
                    {
                        _pinkyRef.SetState(GhostState.CHASE);
                        if (timerRef < 0) timerRef = 20;
                    }
                    else
                    {
                        _pinkyRef.SetState(GhostState.SCATTER);
                        if (timerRef < 0) timerRef = 3;
                    }

                    if (_grunkyRef != null && _grunkyRef.CurrentState == GhostState.SCATTER)
                    {
                        _grunkyRef.SetState(GhostState.CHASE);
                        if (timerRef < 0) timerRef = 20;
                    }
                    else
                    {
                        _grunkyRef.SetState(GhostState.SCATTER);
                        if (timerRef < 0) timerRef = 3;
                    }
                }
            }
        }
        }
    }

    public void PowerPellet()
    {
        frightened = true;
        if (_blinkyRef != null)
        {
            _blinkyRef.SetState(GhostState.STUNNED);
            _blinkyRef._duration = 0.5f;
        }

        if (_pinkyRef != null)
        {
            _pinkyRef.SetState(GhostState.STUNNED);
            _pinkyRef._duration = 0.5f;
        }

        if (_grunkyRef != null)
        {
            _grunkyRef.SetState(GhostState.STUNNED);
            _grunkyRef._duration = 0.5f;
        }
        timerRef = 5f;
    }

    public void Init()
    {
        if (_blinkyRef != null)
        {
            _blinkyRef.SetPosition(_blinkySpawnPosition, "C");
        }

        if (_pinkyRef != null)
        {
            _pinkyRef.SetPosition(_pinkySpawnPosition, "C");
            
        }
        if (_grunkyRef != null)
        {
            _grunkyRef.SetPosition(_grunkySpawnPosition, "C");
        }

        _pacmanRef.SetPosition(_pacmanSpawnPosition);
        _pacmanRef.run = true;
        _init = true;
    }
}
