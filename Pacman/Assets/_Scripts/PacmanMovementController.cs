using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovementController : MonoBehaviour
{
    [Header("Lerp Movement Variables")]
    public Vector2 endPosition;
    public Vector2 startPosition;
    private float elapsedTime;
    private float percentageComplete;
    private float desiredDuration = .5f;
    private Vector2 targetDirection = Vector2.right;
    private Vector2 currentDirection;

    [Header("Components")]
    public PacmanCollisionController colEvents;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + Vector3.right;
    }

    private void Update()
    {
        InputHandler();
        
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector2.Lerp(startPosition, endPosition, percentageComplete);

        if (percentageComplete >= 1)
        {
            startPosition = transform.position;

            if ((targetDirection == Vector2.right && colEvents.rightCol) || (targetDirection == Vector2.down && colEvents.downCol))
            {
                currentDirection = Vector2.zero;
            }
            else
            {
                currentDirection = targetDirection;
            }

            endPosition = transform.position + new Vector3(currentDirection.x, currentDirection.y, 0);
            elapsedTime = 0;
        }
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetDirection = Vector2.up;
        }
    }
}
