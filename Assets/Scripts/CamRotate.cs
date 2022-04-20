using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // �ν����Ϳ��� ���� ����������, �ܺ� ��ũ��Ʈ���� ������ �Ұ����ϰ� �ϱ� ���� ���.
    // ����ȭ : �߻� �����͸� ����, ���� ������ ���·� �ٲٴ� ��.
    // public �����͸� ����ȭ�ϴµ� SerializeField�� ���������ν� private�� ����ȭ �Ǵ� ���̴�.

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
            Debug.Log("���� ī�޶����");
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
        Debug.Log("Y�� ���� X�� �̵� ����");
    }


    public void YMouseRotate()
    {
        float yMouseRotate = Input.GetAxis("Mouse Y") * yMouseSensitivity;

        mouseY += yMouseRotate * Time.deltaTime;
        // P(Position) = P0(Position 0) + vt(velocity�� time) --> ������ġ = ������ġ + (�ӵ�*�ð�)

        mouseY = Mathf.Clamp(mouseY, -90, 90);

        if (mainCam != null)
        {
            mainCam.transform.localRotation = Quaternion.Euler(-mouseY, 0f, 0f);
            //Debug.Log("X�� ���� Y�� �̵� ����");
        }
    }
}
