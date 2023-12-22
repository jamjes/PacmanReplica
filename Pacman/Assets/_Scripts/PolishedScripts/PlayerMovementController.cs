using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    public Vector2 CurrentDirection { get; private set; }

    private float elapsedTime;
    private float desiredDuration = .3f;
    
    private bool isMoving;
    
    [SerializeField] private PathGrid pathGrid;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            targetPosition = currentPosition + playerController.TargetDirection;

            if (isValidPosition(targetPosition))
            {
                isMoving = true;
                CurrentDirection = playerController.TargetDirection;
            }
            else if (!isValidPosition(targetPosition))
            {
                targetPosition = currentPosition + CurrentDirection;
                if (isValidPosition(targetPosition))
                {
                    isMoving = true;
                }
                else
                {
                    if (playerController.CurrentState != PlayerController.PlayerState.Idle)
                    {
                        playerController.SetState(PlayerController.PlayerState.Idle);
                        CurrentDirection = Vector2.zero;
                    }
                        
                }
            }
        }
        else
        {
            if (playerController.CurrentState != PlayerController.PlayerState.Eat)
            {
                playerController.SetState(PlayerController.PlayerState.Eat);
            }

            MovePlayer();
        }
    }

    private bool isValidPosition(Vector2 pos)
    {
        if (currentPosition == pos)
        {
            return false;
        }
        else
        {
            PathNode node = pathGrid.GetNodeAtPosition(pos);
            bool result = (node.NodeType == PathNode.Type.PATH) ? true : false;
            return result;
        }
    }

    private void MovePlayer()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(currentPosition, targetPosition, percentageComplete);

        if (percentageComplete >= 1)
        {
            elapsedTime = 0;
            isMoving = false;
            currentPosition = targetPosition;
        }
    }
}
