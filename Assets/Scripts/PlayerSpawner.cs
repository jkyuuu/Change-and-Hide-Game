using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        var playerObject = PlayerPool.GetPlayer();
        playerObject.transform.position = this.gameObject.transform.position;

        Debug.Log("플레이어 스폰");
    }
}
