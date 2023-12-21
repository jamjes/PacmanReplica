using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovementController m_Controller;

    [SerializeField] private Sprite p_Up, p_Left, p_Down, p_Right;

    [SerializeField] private SpriteRenderer s_Renderer;

    private static readonly int Up = Animator.StringToHash("Up");
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Down = Animator.StringToHash("Down");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private Animator c_Animator;

    private void Start()
    {
        c_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (m_Controller.TargetDirection)
        {
            case Vector2 d when d == Vector2.up:
                s_Renderer.sprite = p_Up;
                s_Renderer.gameObject.transform.localPosition = new Vector2(0, 1.125f);
                break;
            case Vector2 d when d == Vector2.left:
                s_Renderer.sprite = p_Left;
                s_Renderer.gameObject.transform.localPosition = new Vector2(-1.125f, 0);
                break;
            case Vector2 d when d == Vector2.down:
                s_Renderer.sprite = p_Down;
                s_Renderer.gameObject.transform.localPosition = new Vector2(0, -1.125f);
                break;
            case Vector2 d when d == Vector2.right:
                s_Renderer.sprite = p_Right;
                s_Renderer.gameObject.transform.localPosition = new Vector2(1.125f, 0);
                break;
        }
        
        switch (m_Controller.CurrentDirection)
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
}
