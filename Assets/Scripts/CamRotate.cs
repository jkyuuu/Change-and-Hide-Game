using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // �ν����Ϳ��� ���� ����������, �ܺ� ��ũ��Ʈ���� ������ �Ұ����ϰ� �ϱ� ���� ���.
    // ����ȭ : �߻� �����͸� ����, ���� ������ ���·� �ٲٴ� ��.
    // public �����͸� ����ȭ�ϴµ� SerializeField�� ���������ν� private�� ����ȭ �Ǵ� ���̴�.

    [SerializeField]
    private float yMouseSensitivity = 50f;

    Camera tempCam;
    float mouseY;

    private void Start()
    {
        tempCam = FindObjectOfType<Camera>();
        if (tempCam == null)
            Debug.Log("ī�޶����");
    }

    private void Update()
    {
        float yMouseRotate = Input.GetAxis("Mouse Y") * yMouseSensitivity;
        
        mouseY += yMouseRotate * Time.deltaTime;
        // P(Position) = P0(Position 0) + vt(velocity�� time) --> ������ġ = ������ġ + (�ӵ�*�ð�)
        
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        if (tempCam != null)
        {
            tempCam.transform.localRotation = Quaternion.Euler(-mouseY, 0f, 0f);
            Debug.Log("X�� ���� Y�� �̵� ����");
        }
    }
}
