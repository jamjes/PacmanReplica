using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Eat,
        Dead
    };

    public Vector2 TargetDirection; //{ private set; get; }

    public PlayerState CurrentState; //{ private set; get; }

    private void Start()
    {
        CurrentState = PlayerState.Idle;
        TargetDirection = Vector2.zero;
    }

    private void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        TargetDirection = CalculateTargetDirection(hInput, vInput);
    }

    private Vector2 CalculateTargetDirection(float horizontalInput,float verticalInput)
    {
        if (verticalInput == 1)
        {
            return Vector2.up;
        }
        else if (horizontalInput == -1)
        {
            return Vector2.left;
        }
        else if (verticalInput == -1)
        {
            return Vector2.down;
        }
        else if (horizontalInput == 1)
        {
            return Vector2.right;
        }
        else
        {
            return TargetDirection;
        }
    }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }
}
