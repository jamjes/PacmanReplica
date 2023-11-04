using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovementController : MonoBehaviour
{
    private Vector2 endPosition = new Vector2(-7,0);
    private Vector2 startPosition;
    private float desiredDuration = .5f;
    private float elapsedTime;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector2.Lerp(startPosition, endPosition, percentageComplete);
    }
}
