using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;

public class GhostAnimator : MonoBehaviour
{
    private GhostController _ghostRef;
    private Vector2 _direction;
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _body, _eyes, _mouth;
    [SerializeField] private GhostDynamicSpritesheet _spritesheet;

    private Sprite currentEyes, currentMouth;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Chase = Animator.StringToHash("Chase");
    private int _animationState;

    public struct DirectionalSprite
    {
        public Sprite EYES_UP, EYES_LEFT, EYES_DOWN, EYES_RIGHT;
        public Sprite MOUTH;
    }

    public DirectionalSprite DynamicSpriteStandard, DynamicSpriteAngry, DynamicSpriteScared, DynamicSpriteStunned, DynamicSpriteCurrent;

    private void Start()
    {
        _ghostRef = GetComponent<GhostController>();
        DynamicSpriteStandard.EYES_UP = _spritesheet.EYES_STANDARD_UP;
        DynamicSpriteAngry.EYES_UP = _spritesheet.EYES_ANGRY_UP;
        DynamicSpriteScared.EYES_UP = _spritesheet.EYES_SCARED_UP;
        DynamicSpriteStunned.EYES_UP = _spritesheet.EYES_STUNNED;

        DynamicSpriteStandard.EYES_LEFT = _spritesheet.EYES_STANDARD_LEFT;
        DynamicSpriteAngry.EYES_LEFT = _spritesheet.EYES_ANGRY_LEFT;
        DynamicSpriteScared.EYES_LEFT = _spritesheet.EYES_SCARED_LEFT;
        DynamicSpriteStunned.EYES_LEFT = _spritesheet.EYES_STUNNED;

        DynamicSpriteStandard.EYES_DOWN = _spritesheet.EYES_STANDARD_DOWN;
        DynamicSpriteAngry.EYES_DOWN = _spritesheet.EYES_ANGRY_DOWN;
        DynamicSpriteScared.EYES_DOWN = _spritesheet.EYES_SCARED_DOWN;
        DynamicSpriteStunned.EYES_DOWN = _spritesheet.EYES_STUNNED;

        DynamicSpriteStandard.EYES_RIGHT = _spritesheet.EYES_STANDARD_RIGHT;
        DynamicSpriteAngry.EYES_RIGHT = _spritesheet.EYES_ANGRY_RIGHT;
        DynamicSpriteScared.EYES_RIGHT = _spritesheet.EYES_SCARED_RIGHT;
        DynamicSpriteStunned.EYES_RIGHT = _spritesheet.EYES_STUNNED;

        DynamicSpriteStandard.MOUTH = null;
        DynamicSpriteAngry.MOUTH = _spritesheet.MOUTH_ANGRY;
        DynamicSpriteScared.MOUTH = _spritesheet.MOUTH_SCARED;
        DynamicSpriteStunned.MOUTH = _spritesheet.MOUTH_STUNNED;

        DynamicSpriteCurrent = DynamicSpriteStandard;


    }

    private void UpdateOld()
    {
        //_ghostRef.SetCurrentDirection();
        //Debug.Log(_ghostRef.Direction);
        
        /*switch (_ghostRef.CurrentState)
        {
            case GhostState.IDLE:
                _anim.CrossFade(Idle, 0, 0);
                if (_animationState != 0) _animationState = 0;
                break;
            case GhostState.CHASE:
                _anim.CrossFade(Chase, 0, 0);
                if (_animationState != 0) _animationState = 0;
                break;
            case GhostState.SCATTER:
                _anim.CrossFade(Chase, 0, 0);
                if (_animationState != 0) _animationState = 0;
                break;
            case GhostState.ANGRY_CHASE:
                _anim.CrossFade(Chase, 0, 0);
                if (_animationState != 1)
                {
                    _animationState = 1;
                    Debug.Log("Angry");
                }

                break;
            case GhostState.BORED_CHASE:
                _anim.CrossFade(Chase, 0, 0);
                if (_animationState != 2) _animationState = 2;
                break;
            case GhostState.SCARED_CHASE:
                _anim.CrossFade(Chase, 0, 0);
                if (_animationState != 3) _animationState = 3;
                break;
        }

        _direction = _ghostRef.Direction;

        switch (_direction)
        {
            case Vector2 v when v == Vector2.up:
                switch (_animationState)
                {
                    case 0:
                        if (_eyes.sprite != _spritesheet.EYES_STANDARD_UP) _eyes.sprite = _spritesheet.EYES_STANDARD_UP;
                        break;
                    case 1:
                        if (_eyes.sprite != _spritesheet.EYES_ANGRY_UP) _eyes.sprite = _spritesheet.EYES_ANGRY_UP;
                        break;
                    case 2:
                        if (_eyes.sprite != _spritesheet.EYES_BORED_UP) _eyes.sprite = _spritesheet.EYES_BORED_UP;
                        break;
                    case 3:
                        if (_eyes.sprite != _spritesheet.EYES_SCARED_UP) _eyes.sprite = _spritesheet.EYES_SCARED_UP;
                        break;
                }
                break;
            
            case Vector2 v when v == Vector2.left:
                switch (_animationState)
                {
                    case 0:
                        if (_eyes.sprite != _spritesheet.EYES_STANDARD_LEFT) _eyes.sprite = _spritesheet.EYES_STANDARD_LEFT;
                        break;
                    case 1:
                        if (_eyes.sprite != _spritesheet.EYES_ANGRY_LEFT) _eyes.sprite = _spritesheet.EYES_ANGRY_LEFT;
                        break;
                    case 2:
                        if (_eyes.sprite != _spritesheet.EYES_BORED_LEFT) _eyes.sprite = _spritesheet.EYES_BORED_LEFT;
                        break;
                    case 3:
                        if (_eyes.sprite != _spritesheet.EYES_SCARED_LEFT) _eyes.sprite = _spritesheet.EYES_SCARED_LEFT;
                        break;
                }
                break;
                
            case Vector2 v when v == Vector2.down:
                switch (_animationState)
                {
                    case 0:
                        if (_eyes.sprite != _spritesheet.EYES_STANDARD_DOWN) _eyes.sprite = _spritesheet.EYES_STANDARD_DOWN;
                        break;
                    case 1:
                        if (_eyes.sprite != _spritesheet.EYES_ANGRY_DOWN) _eyes.sprite = _spritesheet.EYES_ANGRY_DOWN;
                        break;
                    case 2:
                        if (_eyes.sprite != _spritesheet.EYES_BORED_DOWN) _eyes.sprite = _spritesheet.EYES_BORED_DOWN;
                        break;
                    case 3:
                        if (_eyes.sprite != _spritesheet.EYES_SCARED_DOWN) _eyes.sprite = _spritesheet.EYES_SCARED_DOWN;
                        break;
                }
                break;
            case Vector2 v when v == Vector2.right:
                switch (_animationState)
                {
                    case 0:
                        if (_eyes.sprite != _spritesheet.EYES_STANDARD_RIGHT) _eyes.sprite = _spritesheet.EYES_STANDARD_RIGHT;
                        break;
                    case 1:
                        if (_eyes.sprite != _spritesheet.EYES_ANGRY_RIGHT) _eyes.sprite = _spritesheet.EYES_ANGRY_RIGHT;
                        break;
                    case 2:
                        if (_eyes.sprite != _spritesheet.EYES_BORED_RIGHT) _eyes.sprite = _spritesheet.EYES_BORED_RIGHT;
                        break;
                    case 3:
                        if (_eyes.sprite != _spritesheet.EYES_SCARED_RIGHT) _eyes.sprite = _spritesheet.EYES_SCARED_RIGHT;
                        break;
                }
                break;
        }*/
    }

    private void Update()
    {

    }

    public void PlayAnim(GhostState currentState)
    {
        switch(currentState)
        {
            case GhostState.IDLE:
            
                _anim.CrossFade(Idle, 0, 0);
                break;

            case GhostState.SCATTER:
                _anim.CrossFade(Chase, 0, 0);
                break;

            case GhostState.CHASE:
                if (_body.color == Color.blue)
                {
                    _body.color = _ghostRef.baseColor;
                }
                
                _anim.CrossFade(Chase, 0, 0);
                break;

            case GhostState.STUNNED:
                Stun();
                break;
        }
        
    }

    public void SetDirection(Vector2 dir)
    {
        switch (dir)
        {
            case Vector2 d when d.Equals(Vector2.up):
                if (_eyes.sprite != _spritesheet.EYES_STANDARD_UP) _eyes.sprite = DynamicSpriteCurrent.EYES_UP;
                break;
            case Vector2 d when d.Equals(Vector2.left):
                if (_eyes.sprite != _spritesheet.EYES_STANDARD_LEFT) _eyes.sprite =DynamicSpriteCurrent.EYES_LEFT;
                break;
            case Vector2 d when d.Equals(Vector2.down):
                if (_eyes.sprite != _spritesheet.EYES_STANDARD_DOWN) _eyes.sprite = DynamicSpriteCurrent.EYES_DOWN;
                break;
            case Vector2 d when d.Equals(Vector2.right):
                if (_eyes.sprite != _spritesheet.EYES_STANDARD_RIGHT) _eyes.sprite = DynamicSpriteCurrent.EYES_RIGHT;
                break;
        }
    
    }

    public void Stun()
    {
        if (_body.color != Color.blue)
        {
            _body.color = Color.blue;
            DynamicSpriteCurrent = DynamicSpriteStunned;
            _eyes.sprite = DynamicSpriteCurrent.EYES_DOWN;
            _mouth.sprite =  DynamicSpriteCurrent.MOUTH;
        }
    }

    public void SetDynamicSpriteSheet(string reference)
    {
        switch (reference)
        {
            case "STANDARD":
                DynamicSpriteCurrent = DynamicSpriteStandard;

            break;
            
            case "STUNNED":
                DynamicSpriteCurrent = DynamicSpriteStunned;

            break;
            
            case "SCARED":
                DynamicSpriteCurrent = DynamicSpriteScared;

            break;
            
            case "ANGRY":
                DynamicSpriteCurrent = DynamicSpriteAngry;

            break;
        }

        _mouth.sprite = DynamicSpriteCurrent.MOUTH;
    }
    

}

