using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    private CamView camView;
    public GameObject thirdCamPlayer;
    //private Transform subCam;

    [SerializeField]
    private float xThirdSensitivity = 50f;
    [SerializeField]
    private float yThirdSensitivity = 50f;

    //private Vector3 offset;
    public float xThirdMouse;
    public float yThirdMouse;
    public float distanceZ = 4f;
    public float distanceY = -2f;

    private void Start()
    {
        camView = GetComponent<CamView>();
        //thirdCamPlayer = playerTrans.hitObject;
        //subCam = FindObjectOfType<Transform>();
        //if (subCam == null)
        //{
        //    Debug.Log("서브카메라 없음");
        //}
        //offset = transform.position - thirdPlayer.transform.position;
    }
    private void Update()
    {
        if (camView.toggleView == 3)
        {
            FollowCam();
        }
    }
    //private void LateUpdate()
    //{
    //    transform.position = thirdPlayer.transform.localPosition + offset;
    //}

    public void FollowCam()
    {
        float xThirdRotate = Input.GetAxis("Mouse X") * xThirdSensitivity;
        float yThirdRotate = Input.GetAxis("Mouse Y") * yThirdSensitivity;

        xThirdMouse += xThirdRotate * Time.deltaTime;
        yThirdMouse += yThirdRotate * Time.deltaTime;

        yThirdMouse = Mathf.Clamp(yThirdMouse, -90, -5);
        transform.rotation = Quaternion.Euler(-yThirdMouse, xThirdMouse, 0f);

        Vector3 zDistance = new Vector3(0f, 0f, distanceZ);
        Vector3 yDistance = new Vector3(0f, distanceY, 0f);
        transform.position = thirdCamPlayer.transform.position - (transform.rotation * zDistance + yDistance);
    }

}
