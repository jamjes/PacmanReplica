using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Variables
{
    public enum GhostState
    {
        IDLE,
        CHASE,
        ANGRY_CHASE,
        BORED_CHASE,
        SCARED_CHASE,
        SCATTER,
        STUNNED,
        DEAD
    };

    public enum PelletType
    {
        PELLET,
        POWER_PELLET
    }
}

public interface IGridMover
{
    void SetPosition(Vector2 newPosition, string Index);
    bool MoveToTargetTile(float duration);
    void SetCurrentDirection();
}
