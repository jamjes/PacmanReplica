using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;

public class GhostController : MonoBehaviour, IGridMover
{
    public Vector2 CurrentPosition {private set; get;}
    public Vector2 TargetPosition {private set; get;}
    public Vector2 PreviousPosition {private set; get;}
    public Vector2 Direction {private set; get;}
    public GhostState CurrentState {private set; get;}
    private float _timeRef = 0;
    public GhostAnimator _anim;
    public Color baseColor;
    public bool run {private set; get;}
    
    private void Start() 
    {
        CurrentState = GhostState.IDLE;
        _anim = GetComponent<GhostAnimator>();
        baseColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        run = true;
    }

    private void Update()
    {
       
    }

    //Interface Functions
    public void SetPosition(Vector2 newPosition, string Index)
    {
        if (Index == "P")
        {
            PreviousPosition = newPosition;
        }
        else if (Index == "C")
        {
            CurrentPosition = newPosition;
            transform.position = CurrentPosition;
        }
        else if (Index == "T")
        {
            TargetPosition = newPosition;
        }
    }

    public bool MoveToTargetTile(float duration)
    {
        _timeRef += Time.deltaTime;
        float percentageComplete = _timeRef / duration;
        transform.position = Vector2.Lerp(CurrentPosition, TargetPosition, percentageComplete);
        
        _anim.SetDirection(Direction);


        if (percentageComplete >= 1)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = TargetPosition;
            _timeRef = 0;
            return false;
        }
        else 
        {
            return true;
        }
    }

    public void SetCurrentDirection()
    {
        Direction = TargetPosition - CurrentPosition;
    }

    public void SetState(GhostState newState)
    {
        CurrentState = newState;
        _anim.PlayAnim(CurrentState);
    }

    public void ToggleDisable()
    {
        run = !run;
    }
    public void Disable()
    {
        run = false;
    }
}


