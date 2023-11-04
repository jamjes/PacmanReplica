using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovementController : MonoBehaviour
{
    private Vector2 endPosition = new Vector2(-7,0);
    private Vector2 startPosition;
    private float desiredDuration = .5f;
    private float elapsedTime;
    private bool move = true;
    private float percentageComplete;
    public Vector2 targetDirection = Vector2.right;
    public Vector2 currentDirection;
    private int timeflow = 1;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        InputHandler();
        
        if (timeflow == 1)
        {
            elapsedTime += Time.deltaTime * timeflow;
            percentageComplete = elapsedTime / desiredDuration;

            transform.position = Vector2.Lerp(startPosition, endPosition, percentageComplete);
        }

        if (percentageComplete >= 1)
        {
            startPosition = transform.position;
            currentDirection = targetDirection;
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
