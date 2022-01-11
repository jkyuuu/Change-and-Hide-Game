using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string zInputName = "Vertical";
    public string xInputName = "Horizontal";
    public string rayButtonName = "Fire1";

    // 값 할당은 내부에서만
    public float zInput { get; private set; }
    public float xInput { get; private set; }
    public bool ray { get; private set; }


    private void Update()
    {
        zInput = Input.GetAxis(zInputName);
        xInput = Input.GetAxis(xInputName);
        ray = Input.GetButton(rayButtonName);
    }

}
