using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // 인스펙터에서 접근 가능하지만, 외부 스크립트에서 접근이 불가능하게 하기 위해 사용.
    // 직렬화 : 추상 데이터를 전송, 저장 가능한 형태로 바꾸는 것.
    // public 데이터만 직렬화하는데 SerializeField를 선언함으로써 private도 직렬화 되는 것이다.

    [SerializeField]
    private float xMouseSensitivity = 5f;
    [SerializeField]
    private float yMouseSensitivity = 50f;

    public Camera mainCam;
    public float mouseY;

    public void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        if (mainCam == null)
        {
            Debug.Log("메인 카메라없음");
        }

    }

    private void Update()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            XMouseRotate();
        }

        YMouseRotate();
    }

    private void XMouseRotate()
    {
        float xMouseRotate = Input.GetAxis("Mouse X") * xMouseSensitivity;

        transform.Rotate(0f, xMouseRotate, 0f);
        Debug.Log("Y축 기준 X축 이동 감지");
    }


    public void YMouseRotate()
    {
        float yMouseRotate = Input.GetAxis("Mouse Y") * yMouseSensitivity;

        mouseY += yMouseRotate * Time.deltaTime;
        // P(Position) = P0(Position 0) + vt(velocity와 time) --> 변경위치 = 현재위치 + (속도*시간)

        mouseY = Mathf.Clamp(mouseY, -90, 90);

        if (mainCam != null)
        {
            mainCam.transform.localRotation = Quaternion.Euler(-mouseY, 0f, 0f);
            //Debug.Log("X축 기준 Y축 이동 감지");
        }
    }
}
