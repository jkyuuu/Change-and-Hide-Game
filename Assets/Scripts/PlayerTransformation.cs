using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    public enum State
    {
        Ready,
        Transformation
    }

    public State state { get; private set; } // 현재 플레이어 상태

    private void OnEnable()
    {
        state = State.Ready;
    }
}
