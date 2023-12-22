using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private PlayerMovementController MovementController;
    [SerializeField] private SpriteRenderer SprRenderer;
    [SerializeField] private Animator c_Animator;
    [SerializeField] private Sprite p_Up, p_Left, p_Down, p_Right;

    private static readonly int Up = Animator.StringToHash("Up");
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Down = Animator.StringToHash("Down");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Update()
    {
        UpdateAnim();
        UpdatePointer();
    }

    private void UpdateAnim()
    {
        switch (MovementController.CurrentDirection)
        {
            case Vector2 d when d == Vector2.up:
                c_Animator.CrossFade(Up, 0, 0);
                break;
            case Vector2 d when d == Vector2.left:
                c_Animator.CrossFade(Left, 0, 0);
                break;
            case Vector2 d when d == Vector2.down:
                c_Animator.CrossFade(Down, 0, 0);
                break;
            case Vector2 d when d == Vector2.right:
                c_Animator.CrossFade(Right, 0, 0);
                break;
            case Vector2 d when d == Vector2.zero:
                c_Animator.CrossFade(Idle, 0, 0);
                break;
        }
    }
    private void UpdatePointer()
    {
        switch (PlayerController.TargetDirection)
        {
            case Vector2 d when d == Vector2.up:
                SprRenderer.sprite = p_Up;
                SprRenderer.gameObject.transform.localPosition = new Vector2(0, 1.125f);
                break;
            case Vector2 d when d == Vector2.left:
                SprRenderer.sprite = p_Left;
                SprRenderer.gameObject.transform.localPosition = new Vector2(-1.125f, 0);
                break;
            case Vector2 d when d == Vector2.down:
                SprRenderer.sprite = p_Down;
                SprRenderer.gameObject.transform.localPosition = new Vector2(0, -1.125f);
                break;
            case Vector2 d when d == Vector2.right:
                SprRenderer.sprite = p_Right;
                SprRenderer.gameObject.transform.localPosition = new Vector2(1.125f, 0);
                break;
        }
    }
}
