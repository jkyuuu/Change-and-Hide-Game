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

    public State state { get; private set; } // ���� �÷��̾� ����

    private void OnEnable()
    {
        state = State.Ready;
    }
}
