using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string zInputName = "Vertical";
    public string xInputName = "Horizontal";
    //public string selectButtonName = "Fire1";

    // 값 할당은 내부에서만
    public float zInput { get; private set; }
    public float xInput { get; private set; }
    public bool selectObject { get; private set; }
    public bool returnPlayer { get; private set; }


    private void Update()
    {
        xInput = Input.GetAxis(xInputName);
        zInput = Input.GetAxis(zInputName);
        selectObject = Input.GetMouseButtonDown(0);
        returnPlayer = Input.GetKeyDown("c");
    }

}
