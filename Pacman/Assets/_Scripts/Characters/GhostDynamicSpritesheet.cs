using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Spritesheet", menuName = "ScriptableObjects/CustomAnimation/DynamicSpritesheet", order = 1)]
public class GhostDynamicSpritesheet : ScriptableObject
{
    [Header("Standard Features")]
    public Sprite EYES_STANDARD_UP;
    public Sprite EYES_STANDARD_LEFT;
    public Sprite EYES_STANDARD_DOWN;
    public Sprite EYES_STANDARD_RIGHT;

    [Header("Angry Features")]
    public Sprite EYES_ANGRY_UP;
    public Sprite EYES_ANGRY_LEFT;
    public Sprite EYES_ANGRY_DOWN;
    public Sprite EYES_ANGRY_RIGHT;
    public Sprite MOUTH_ANGRY;

    [Header("Scared Features")]
    public Sprite EYES_SCARED_UP;
    public Sprite EYES_SCARED_LEFT;
    public Sprite EYES_SCARED_DOWN;
    public Sprite EYES_SCARED_RIGHT;
    public Sprite MOUTH_SCARED;

    [Header("Scared Features")]
    public Sprite EYES_BORED_UP;
    public Sprite EYES_BORED_LEFT; 
    public Sprite EYES_BORED_DOWN;
    public Sprite EYES_BORED_RIGHT; 
    public Sprite MOUTH_BORED;     

    [Header("Stunned Features")]
    public Sprite EYES_STUNNED;
    public Sprite MOUTH_STUNNED;       
}
