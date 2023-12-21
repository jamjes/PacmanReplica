using UnityEngine;

[CreateAssetMenu(fileName = "New Ghost Settings", menuName = "ScriptableObjects/GhostSettings", order = 1)]
public class GhostSettings : ScriptableObject
{
    [Header("Behaviour")]
    public bool ANGRY;
    public bool SCARED;
    public bool BORED;    
}
