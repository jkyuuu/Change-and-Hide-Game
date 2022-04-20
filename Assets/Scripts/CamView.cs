using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamView : MonoBehaviour
{
    private PlayerTransformation playerTransformation;

    public int toggleView = 1;
    public float distance = 4f;

    public GameObject firstPlayer;
    public GameObject thirdPlayer;

    private void Start()
    {
        playerTransformation = GameObject.FindWithTag("Player").GetComponent<PlayerTransformation>();
    }
    private void Update()
    {
        if (toggleView == 1)
        {
            firstPlayer = playerTransformation.playerObject;
            FirstView();
        }
        else if (toggleView == 3)
        {
            thirdPlayer = playerTransformation.hitObject;
            if(thirdPlayer != null)
            {
                ThirdView();
            }
        }
    }
    public void FirstView()
    {
        Vector3 reverseDistance = new Vector3(0.0f, 0.4f, 0.2f);
        Vector3 firstLocation = new Vector3(0f, 1f, 0f);
        transform.position = (firstPlayer.transform.position + firstLocation) + transform.rotation * reverseDistance;
    }
    public void ThirdView()
    {
        Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
        transform.position = thirdPlayer.transform.position - transform.rotation * reverseDistance;
    }
}
