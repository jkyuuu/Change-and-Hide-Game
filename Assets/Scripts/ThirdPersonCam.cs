using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public GameObject thirdPlayer;
    private PlayerTransformation playerTransformation;
    private Transform subCam;

    [SerializeField]
    private float xThirdSensitivity = 50f;
    [SerializeField]
    private float yThirdSensitivity = 50f;

    private Vector3 offset;
    public float xThirdMouse = 0;
    public float yThirdMouse = 0;
    public float distanceZ = 4f;
    public float distanceY = -2f;


    private void OnEnable()
    {
        thirdPlayer = GameObject.FindWithTag("Transformed").gameObject;
    }
    private void Start()
    {
        playerTransformation = GetComponent<PlayerTransformation>();

        subCam = FindObjectOfType<Transform>();
        if (subCam == null)
        {
            Debug.Log("서브카메라 없음");
        }
        //offset = transform.position - thirdPlayer.transform.position;
    }
    void Update()
    {
        //subCam.LookAt(thirdPlayer.transform);

        float xThirdRotate = Input.GetAxis("Mouse X") * xThirdSensitivity;
        float yThirdRotate = Input.GetAxis("Mouse Y") * yThirdSensitivity;

        xThirdMouse += xThirdRotate * Time.deltaTime;
        yThirdMouse += yThirdRotate * Time.deltaTime;

        yThirdMouse = Mathf.Clamp(yThirdMouse, -90, -5);
        transform.rotation = Quaternion.Euler(-yThirdMouse, xThirdMouse, 0f);

        Vector3 zDistance = new Vector3(0f, 0f, distanceZ);
        Vector3 yDistance = new Vector3(0f, distanceY, 0f);
        transform.position = thirdPlayer.transform.position - (transform.rotation * zDistance + yDistance);
    }
    //private void LateUpdate()
    //{
    //    transform.position = thirdPlayer.transform.localPosition + offset;
    //}
}
