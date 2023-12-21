using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 currentPosition, targetPosition;
    public Vector2 TargetDirection { get; private set; }
    public Vector2 CurrentDirection { get; private set; }
    private float elapsedTime, desiredDuration = .3f;
    private bool isMoving;
    [SerializeField] private PathGrid grid;

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TargetDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            TargetDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TargetDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            TargetDirection = Vector2.right;
        }


        if (!isMoving)
        {
            targetPosition = currentPosition + TargetDirection;

            if (isValidPosition(targetPosition))
            {
                isMoving = true;
                CurrentDirection = TargetDirection;
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
                    CurrentDirection = Vector2.zero;
                }
            }
        }
        else
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

    private bool isValidPosition(Vector2 pos)
    {
        PathNode node = grid.GetNodeAtPosition(pos);
        bool result = (node.NodeType == PathNode.Type.PATH) ? true : false;
        return result;
    }
}
