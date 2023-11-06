using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PacmanCollisionController : MonoBehaviour
{
    public float raycastDistance;
    public bool rightCol = false, leftCol = false, upCol = false, downCol = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Wall Collision Detections
        
        Vector2 pos = transform.position;
        RaycastHit2D[] rightHit = Physics2D.RaycastAll(transform.position, Vector2.right, raycastDistance);
        RaycastHit2D[] downHit = Physics2D.RaycastAll(transform.position, Vector2.down, raycastDistance);
        RaycastHit2D[] leftHit = Physics2D.RaycastAll(transform.position, Vector2.left, raycastDistance);
        RaycastHit2D[] upHit = Physics2D.RaycastAll(transform.position, Vector2.up, raycastDistance);

        foreach(var hit in rightHit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                rightCol = true;
                break;
            }
            else
            {
                rightCol = false;
            }
        }

        foreach (var hit in leftHit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                leftCol = true;
                break;
            }
            else
            {
                leftCol = false;
            }
        }

        foreach (var hit in upHit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                upCol = true;
                break;
            }
            else
            {
                upCol = false;
            }
        }

        foreach (var hit in downHit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                downCol = true;
                break;
            }
            else
            {
                downCol = false;
            }
        }
    }
}
